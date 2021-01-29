using LyricsCollector.Controllers;
using LyricsCollector.Models.Contracts;
using LyricsCollector.Models.LyricsModels;
using LyricsCollector.Services.Contracts;
using LyricsCollector.Services.Contracts.IDbHelpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace LyricsCollectorAPI.Tests
{
    public class Tests
    {
        private LyricsController controller;
        private Mock<ILyricsService> lyricsServiceMock;
        private Mock<IDbLyrics> dbLyricsMock;
        private Mock<ILyricsResponseModel> lyricsResponseMock;
        private Mock<ILyricsPostModel> lyricsPostMock;


        [SetUp]
        public void Setup()
        {
            lyricsServiceMock = new Mock<ILyricsService>();
            dbLyricsMock = new Mock<IDbLyrics>();
            
            lyricsResponseMock = new Mock<ILyricsResponseModel>();
        }

        [Test]
        public async Task GetLyricsAsync_ReturnsLyricsResponseModel()
        {
            //Arrange

            string testArtist = "Patti Smith";
            string testTitle = "Land";

            var lyricsPM = new LyricsPostModel()
            {
                Artist = testArtist,
                Title = testTitle
            };

            //Act
            var result = await controller.GetLyricsAsync(lyricsPM);

            //Assert


            dbLyricsMock.Verify(d => d.SaveLyricsToDbAsync(lyricsResponseMock.Object), Times.Once);
            lyricsServiceMock.Verify(l => l.Notify(lyricsResponseMock.Object), Times.Once);
            //Assert.IsInstanceOf(result, ok);
        }
    }
}