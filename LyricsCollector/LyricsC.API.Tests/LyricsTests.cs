using LyricsCollector.Controllers;
using LyricsCollector.Models.LyricsModels;
using LyricsCollector.Models.LyricsModels.Contracts;
using LyricsCollector.Models.SpotifyModels;
using LyricsCollector.Models.SpotifyModels.Contracts;
using LyricsCollector.Services.Contracts;
using LyricsCollector.Services.Contracts.IDbHelpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LyricsC.API.Tests
{
    [TestFixture]
    class LyricsTests
    {
        private LyricsController controller;
        private Mock<ILyricsService> lyricsServiceMock;
        private Mock<IDbLyrics> dbLyricsMock;
        private Mock<ISpotifyService> spotifyServiceMock;
        private Mock<ICollectionService> collectionServiceMock;
        private Mock<ILyricsResponseModel> lyricsRmMock;
        private Mock<ITrackResponseModel> trackRmMock;
        //private Mock<ITrack> trackMock;
        //private IItem[] items;
        //private Mock<IAlbum> albumMock;
        //private IImage[] images;
        //private Mock<IExternal_Urls> extUrlsMock;

        [SetUp]
        public void Setup()
        {
            lyricsServiceMock = new Mock<ILyricsService>();
            dbLyricsMock = new Mock<IDbLyrics>();
            spotifyServiceMock = new Mock<ISpotifyService>();
            collectionServiceMock = new Mock<ICollectionService>();

            var testArtist = "testArtist";
            lyricsRmMock = new Mock<ILyricsResponseModel>();
            lyricsRmMock.Setup(l => l.Artist).Returns(testArtist);

            //var imageMock = new Mock<IImage>();
            //imageMock.Setup(i => i.Url).Returns("coverImgUrl");

            //images = new IImage[]
            //{
            //    imageMock.Object
            //};

            //albumMock = new Mock<IAlbum>();
            //albumMock.Setup(a => a.Images).Returns(images);

            //extUrlsMock = new Mock<IExternal_Urls>();
            //extUrlsMock.Setup(e => e.Spotify).Returns("spotifyLink");

            //var itemMock = new Mock<IItem>();
            //itemMock.Setup(item => item.External_urls).Returns(extUrlsMock.Object);

            var img = "testCoverUrl";
            var testUrl = "testSpotifyLink";
            var extUrl = new External_Urls() { Spotify = testUrl };

            var images = new Image[]
            {
                new Image() { Url = img }
            };

            var album = new Album() { Images = images };
            var item = new Item() { Album = album, External_urls = extUrl };
            var items = new Item[] { item };
            var track = new Track { Items = items };

            trackRmMock = new Mock<ITrackResponseModel>();
            trackRmMock.Setup(track => track.Track).Returns(track);

            controller = new LyricsController (lyricsServiceMock.Object, spotifyServiceMock.Object, 
                dbLyricsMock.Object, collectionServiceMock.Object);
        }

        [Test]
        public async Task GetLyricsAsync_BadInput_ReturnsNotFound()
        {
            //Arrange
            var lyricsPM = new LyricsPostModel()
            {
                Artist = "artist",
                Title = "title"
            };

            dbLyricsMock.Setup(d => d.LyricsInDbMatch(It.IsAny<string>(), It.IsAny<string>())).Returns(null as ILyricsResponseModel);
            lyricsServiceMock.Setup(l => l.SearchAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(null as ILyricsResponseModel));
            
            //Act
            var result = await controller.GetLyricsAsync(lyricsPM);

            //Assert
            dbLyricsMock.Verify(d => d.LyricsInDbMatch(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            lyricsServiceMock.Verify(l => l.SearchAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetLyricsAsync_ValidInput_ReturnsOkResult()
        {
            //Arrange
            var lyricsPM = new LyricsPostModel()
            {
                Artist = "artist",
                Title = "title"
            };

            dbLyricsMock.Setup(d => d.LyricsInDbMatch(It.IsAny<string>(), It.IsAny<string>())).Returns(null as ILyricsResponseModel);
            lyricsServiceMock.Setup(l => l.SearchAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(lyricsRmMock.Object));
            spotifyServiceMock.Setup(s => s.SearchAsync(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(trackRmMock.Object));

            //Act
            var result = await controller.GetLyricsAsync(lyricsPM);

            //Assert
            lyricsServiceMock.Verify(l => l.ToTitleCase(It.IsAny<string>()), Times.Exactly(2));
            dbLyricsMock.Verify(d => d.LyricsInDbMatch(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            lyricsServiceMock.Verify(l => l.SearchAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            spotifyServiceMock.Verify(s => s.SearchAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            dbLyricsMock.Verify(d => d.SaveLyricsToDbAsync(It.IsAny<ILyricsResponseModel>()), Times.Once);
            lyricsServiceMock.Verify(l => l.Notify(lyricsRmMock.Object), Times.Once);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        //dbLyricsMock.Setup(d => d.LyricsInDbMatch(It.IsAny<string>(), It.IsAny<string>())).Returns(lyricsRmMock.Object);
    }
}
