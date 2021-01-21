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
        public async Task<IActionResult> GetCollection(int collectionId)
        {
            try
            {
                var collection = await _collectionService.GetCollectionAsync(collectionId);
                return Ok(collection);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("Collections")]
        public async Task<IActionResult> GetAllUsersCollections()
        {
            try
            {
                var collection = await _collectionService.GetAllCollectionsAsync();
                return Ok(collection);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}
