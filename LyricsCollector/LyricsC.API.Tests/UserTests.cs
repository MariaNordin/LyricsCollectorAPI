using LyricsCollector.Controllers;
using LyricsCollector.Entities;
using LyricsCollector.Entities.Contracts;
using LyricsCollector.Models.Contracts;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Services.Contracts;
using LyricsCollector.Services.Contracts.IDbHelpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace LyricsC.API.Tests
{
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

            controller = new UserController(userServiceMock.Object, dbUsersMock.Object, userMock.Object);
        }

        [Test]
        public async Task RegisterAsync_NotValidModelReturnsBadRequest()
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
            Assert.IsNotNull(result);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }



        //[Test]
        //public async Task RegisterAsync_NotUniqueValuesReturnsBadRequest()
        //{
        //    //Arrange
        //    var userPM = new UserPostModel()
        //    {
        //        Name = "test",
        //        Email = "test",
        //        Password = "test"
        //    };

        //    userMock.Setup(user => user.Name).Returns("User");

        //    //Act
        //    var result = await controller.RegisterAsync(userPM);
        //    var badRequestResult = result as BadRequestResult;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(400, badRequestResult.StatusCode);
        //}
    }
}