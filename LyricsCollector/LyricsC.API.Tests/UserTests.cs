using LyricsCollector.Controllers;
using LyricsCollector.Entities.Contracts;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Models.UserModels.Contracts;
using LyricsCollector.Services.Contracts;
using LyricsCollector.Services.Contracts.IDbHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace LyricsC.API.Tests
{
    [TestFixture]
    public class Tests
    {
        private UserController controller;

        private Mock<IUserService> userServiceMock;
        private Mock<IDbUsers> dbUsersMock;
        private Mock<IUserPostModel> userPmMock;
        private Mock<IUser> userMock;
        private Mock<IUserToken> tokenMock;

        [SetUp]
        public void Setup()
        {
            userServiceMock = new Mock<IUserService>();
            dbUsersMock = new Mock<IDbUsers>();

            var testUser = "testUser";
            var fakeToken = "gsjak3745gh";

            userPmMock = new Mock<IUserPostModel>();
            userPmMock.Setup(user => user.Name).Returns(testUser);
            
            userMock = new Mock<IUser>();
            userMock.Setup(user => user.Name).Returns(testUser);

            tokenMock = new Mock<IUserToken>();
            tokenMock.Setup(token => token.Token).Returns(fakeToken);

            controller = new UserController(userServiceMock.Object, dbUsersMock.Object, userMock.Object);
        }

        [Test]
        public async Task RegisterAsync_NotValidModel_ReturnsBadRequest()
        {
            //Arrange
            var userPM = new UserPostModel()
            {
                Name = "",
                Email = "",
                Password = ""
            };

            //Act
            var result = await controller.RegisterAsync(userPM);
            var badRequestResult = result as BadRequestResult;

            //Assert
            dbUsersMock.Verify(db => db.GetUserAsync(userPM.Name), Times.Never);
            userServiceMock.Verify(u => u.GeneratePassword(userPmMock.Object), Times.Never);
            dbUsersMock.Verify(db => db.SaveUserAsync(It.IsAny<IUser>()), Times.Never);
            Assert.IsNotNull(result);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public async Task RegisterAsync_ValidModel_CallsInnerMethods()
        {
            //Arrange
            var userPM = new UserPostModel()
            {
                Name = "name",
                Email = "email",
                Password = "password"
            };

            //Act
            var result = await controller.RegisterAsync(userPM);

            //Assert
            dbUsersMock.Verify(db => db.GetUserAsync(userPM.Name), Times.Once);
            userServiceMock.Verify(u => u.GeneratePassword(It.IsAny<IUserPostModel>()), Times.Once);
            dbUsersMock.Verify(db => db.SaveUserAsync(It.IsAny<IUser>()), Times.Once);
        }

        [Test]
        public async Task LoginAsync_NoExistingUser_ReturnsNotFound()
        {
            //Arrange
            var userPM = new UserPostModel()
            {
                Name = "name",
                Email = "email",
                Password = "password"
            };

            //Act
            var result = await controller.LoginAsync(userPM);
            var notFoundResult = result as NotFoundResult;

            //Assert
            userServiceMock.Verify(u => u.ValidatePassword(userPmMock.Object, userMock.Object), Times.Never);
            Assert.IsNotNull(result);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public async Task LoginAsync_ValidUser_ReturnsOk()
        {
            //Arrange
            var userPM = new UserPostModel()
            {
                Name = "name",
                Email = "email",
                Password = "password"
            };

            dbUsersMock.Setup(d => d.GetUserAsync(It.IsAny<string>())).Returns(Task.FromResult(userMock.Object));
            userServiceMock.Setup(u => u.ValidatePassword(It.IsAny<IUserPostModel>(), It.IsAny<IUser>())).Returns(tokenMock.Object);

            //Act
            var result = await controller.LoginAsync(userPM);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void GetUserAsync_HasAuthorizeAttribute()
        {
            //Arrange
            //Act
            var attribute = controller.GetType().GetMethod("GetUserAsync").GetCustomAttributes(typeof(AuthorizeAttribute), true);

            //Assert
            Assert.AreEqual(typeof(AuthorizeAttribute), attribute[0].GetType());
        }
    }
}