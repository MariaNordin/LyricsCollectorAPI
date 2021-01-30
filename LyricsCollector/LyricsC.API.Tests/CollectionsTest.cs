using LyricsCollector.Controllers;
using LyricsCollector.Models.CollectionModels;
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
    public class CollectionsTest
    {
        private CollectionController controller;
        private Mock<ICollectionService> collectionServiceMock;
        private Mock<IDbCollections> dbCollectionsMock;

        [SetUp]
        public void Setup()
        {
            collectionServiceMock = new Mock<ICollectionService>();
            dbCollectionsMock = new Mock<IDbCollections>();

            controller = new CollectionController(dbCollectionsMock.Object, collectionServiceMock.Object);
        }
        
        [Test]
        public void CollectionController_HasAuthorizeAttribute()
        {
            //Arrange
            //Act
            var attribute = controller.GetType().GetCustomAttributes(typeof(AuthorizeAttribute), true);

            //Assert
            Assert.AreEqual(typeof(AuthorizeAttribute), attribute[0].GetType());
        }

        [Test]
        public async Task GetCollectionAsync_NotValidModel_ReturnsNotFound()
        {
            //Arrange
            var collectionPM = new CollectionPostModel();

            //Act
            var result = await controller.GetCollectionAsync(collectionPM);
            var notFoundResult = result as NotFoundResult;

            //Assert
            dbCollectionsMock.Verify(db => db.GetCollectionAsync(collectionPM.Id), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public async Task SaveToCollectionAsync_SaveFails_ReturnsBadRequest()
        {
            //Arrange
            var collectionPM = new CollectionPostModel();

            //Act
            var result = await controller.SaveToCollectionAsync(collectionPM);

            //Assert
            collectionServiceMock.Verify(c => c.GetCurrentLyrics(), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
