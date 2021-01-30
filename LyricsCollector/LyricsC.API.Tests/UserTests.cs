using LyricsCollector.Controllers;
using LyricsCollector.Entities.Contracts;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Services.Contracts;
using LyricsCollector.Services.Contracts.IDbHelpers;
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
        private Mock<IUser> userMock;


        [SetUp]
        public void Setup()
        {
            userServiceMock = new Mock<IUserService>();
            dbUsersMock = new Mock<IDbUsers>();
            userMock = new Mock<IUser>();

            userMock.SetupAllProperties();
            userMock.Setup(user => user.Name).Returns("name");
            userMock.Setup(user => user.Email).Returns("email");

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
            userServiceMock.Verify(u => u.GeneratePassword(userPM), Times.Never);
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
            userServiceMock.Verify(u => u.GeneratePassword(userPM), Times.Once);
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
            userServiceMock.Verify(u => u.ValidatePassword(userPM, userMock.Object), Times.Never);
            Assert.IsNotNull(result);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        //[Test]
        //public async Task RegisterAsync_ValidModel_ReturnsOkResult()
        //{
        //    //Arrange
        //    var userPM = new UserPostModel()
        //    {
        //        Name = "test",
        //        Email = "test",
        //        Password = "test"
        //    };

        //    dbUsersMock.Setup(db => db.SaveUserAsync(It.IsAny<IUser>())).Returns(new Task<bool>(() => true));

        //    //Act
        //    var result = await controller.RegisterAsync(userPM);
        //    var okResult = result as OkResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(200, okResult.StatusCode);
        //}
    }
}