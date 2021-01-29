using LyricsCollector.Services.Contracts;
using Moq;
using NUnit.Framework;

namespace LyricsCollectorAPI.Tests
{
    public class Tests
    {
        private Mock<ICollectionService> collectionServiceMock;

        [SetUp]
        public void Setup()
        {
            collectionServiceMock = new Mock<ICollectionService>();
        }

        [Test]
        public void GetLyricsAsync_ReturnsLyrics()
        {

            Assert.Pass();
        }
    }
}