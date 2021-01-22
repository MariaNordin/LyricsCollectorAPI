using LyricsCollector.Entities;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace LyricsCollector.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CORSPolicy")]
    [ApiController]
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Collection")]
        public async Task<IActionResult> GetCollectionAsync([FromBody] UserPostModel userPM)
        {
            try
            {
                var collection = await _collectionService.GetCollectionAsync(userPM.CollectionId, userPM.Email);
                return Ok(collection);
            }
            catch (System.Exception)
            {
                return BadRequest(); 
            }
        }

        [HttpPost("AllCollections")]
        public async Task<IActionResult> GetUsersCollectionsAsync([FromBody] UserPostModel userPM)
        {
            try
            {
                var collections = await _collectionService.GetAllCollectionsAsync(userPM.Email);
                return Ok(collections);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("NewCollection")]
        public async Task<IActionResult> CreateNewCollectionAsync([FromBody] UserPostModel userPM)
        {
            try
            {
                var collection = await _collectionService.NewCollection(userPM.NewCollectionName, userPM.Email);
                return Ok(collection);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}
