using LyricsCollector.Controllers;
using LyricsCollector.Models.SpotifyModels.Contracts;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace LyricsC.API.Tests
{
    [TestFixture]
    class SpotifyTests
    {
        private SpotifyController controller;
        private Mock<ISpotifyService> spotifyServiceMock;
        private Mock<ISpotifyTokenModel> tokenMock;

        [SetUp]
        public void Setup()
        {
            spotifyServiceMock = new Mock<ISpotifyService>();

            var testToken = "ajdsö748394ajskk87";
            tokenMock = new Mock<ISpotifyTokenModel>();           
            tokenMock.Setup(token => token.Access_token).Returns(testToken);

            controller = new SpotifyController(spotifyServiceMock.Object);
        }

        [Test]
        public async Task GetTokenAsync_Calls_GetAccessToken()
        {
            //Arrange
            //Act
            var token = await controller.GetTokenAsync();

            //Assert
            spotifyServiceMock.Verify(s => s.GetAccessTokenAsync(), Times.Once);
        }

        [Test]
        public async Task GetTokenAsync_ReturnsOkResult()
        {
            //Arrange           
            spotifyServiceMock.Setup(s => s.GetAccessTokenAsync()).Returns(Task.FromResult(tokenMock.Object));

            //Act
            var token = await controller.GetTokenAsync();

            //Assert
            Assert.IsNotNull(token);
            Assert.IsInstanceOf<OkObjectResult>(token);
        }
    }
}
