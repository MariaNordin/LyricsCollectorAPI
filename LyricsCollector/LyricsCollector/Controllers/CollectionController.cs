using LyricsCollector.Entities;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LyricsCollector.Controllers
{
    public class CollectionController : ControllerBase
    {
        // GET: Hämta alla listor
        // GET: Hämta en lista
        // POST: Skapa ny lista
        // POST: Lägg till låt i lista
        // DELETE: Ta bort lista 

        private readonly ICollectionService _collectionService;
        //private Collection _collection;

        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet("Collection")]
        public async Task<IActionResult> GetCollection(int collectionId, int userId)
        {
            try
            {
                var collection = await _collectionService.GetCollectionAsync(collectionId, userId);
                return Ok(collection);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("AllCollections")]
        public async Task<IActionResult> GetAllUsersCollections(int userId)
        {
            try
            {
                var collections = await _collectionService.GetAllCollectionsAsync(userId);
                return Ok(collections);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("NewCollection")]
        public async Task<IActionResult> NewCollection(string name, int userId)
        {
            try
            {
                var collection = await _collectionService.NewCollection(name, userId);
                return Ok(collection);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}
