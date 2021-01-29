using LyricsCollector.Controllers;
using LyricsCollector.Models.Contracts;
using LyricsCollector.Models.LyricsModels;
using LyricsCollector.Services.Contracts;
using LyricsCollector.Services.Contracts.IDbHelpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LyricsCollectorAPI_Tests
{
    public class UnitTest1
    {

        //[Fact]
        //public async Task GetLyricsAsync_ReturnsLyricsResponseModelFromDb()
        //{
        //    //Arrange
        //    string testArtist = "Patti Smith";
        //    string testTitle = "Land";

        //    var lyricsServiceMock = new Mock<ILyricsService>();
        //    var dbLyricsMock = new Mock<IDbLyrics>();
        //    var spotifyServiceMock = new Mock<ISpotifyService>();
        //    var collectionServiceMock = new Mock<ICollectionService>();

        //    var lyricsResponseMock = new Mock<ILyricsResponseModel>();
        //    lyricsResponseMock.Setup(item => item.Artist).Returns(testArtist);
        //    lyricsResponseMock.Setup(item => item.Title).Returns(testTitle);
        //    lyricsResponseMock.Setup(item => item.Lyrics).Returns("Lyrics");
        //    lyricsResponseMock.Setup(item => item.CoverImage).Returns("CoverImg");
        //    lyricsResponseMock.Setup(item => item.SpotifyLink).Returns("Spotify");


        //    var controller = new LyricsController(lyricsServiceMock.Object, spotifyServiceMock.Object, dbLyricsMock.Object, collectionServiceMock.Object);

        //    var lyricsPM = new LyricsPostModel()
        //    {
        //        Artist = testArtist,
        //        Title = testTitle
        //    };

        //    dbLyricsMock.Setup(db => db.LyricsInDbMatch(testArtist, testTitle)).Returns(lyricsResponseMock.Object);

            //Act
            //var result = await controller.GetLyricsAsync(lyricsPM);

            //Assert
            //lyricsServiceMock.Verify(l => l.Notify(lyricsResponseMock.Object), Times.Once);
            //lyricsServiceMock.Verify(l => l.Search(testArtist, testTitle), Times.Never);
            //spotifyServiceMock.Verify(s => s.Search(testArtist, testTitle), Times.Never);
            //dbLyricsMock.Verify(d => d.SaveLyricsToDbAsync(lyricsResponseMock.Object), Times.Never);

            //var okResult = Assert.IsType<OkObjectResult>(result);
            //var responsLyrics = Assert.IsType<LyricsResponseModel>(okResult.Value);

            //Assert.Equal(testArtist, responsLyrics.Artist);
            //Assert.Equal(testTitle, responsLyrics.Title);
            //Assert.NotNull(responsLyrics.Lyrics);
            //Assert.NotNull(responsLyrics.CoverImage);
            //Assert.NotNull(responsLyrics.SpotifyLink);
        //}
    }
}
