using LyricsCollector.Controllers;
using LyricsCollector.Entities;
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


        [SetUp]
        public void Setup()
        {
            userServiceMock = new Mock<IUserService>();
            dbUsersMock = new Mock<IDbUsers>();

            //var userPM = new UserPostModel()
            //{
            //    Name = "name",
            //    Email = "email",
            //    Password = "password"
            //};

            controller = new UserController(userServiceMock.Object, dbUsersMock.Object);

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
    }
}