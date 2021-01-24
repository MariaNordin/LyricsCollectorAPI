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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
  
        [HttpPost("Collection")] //?? behöver jag denna? beror på hur jag hämtar listor och låtar - när user loggar in eller när de frågas efter
        public async Task<IActionResult> GetCollectionAsync(int collectionId)
        {
            var userName = HttpContext.User.Identity.Name;
            try
            {
                var collection = await _collectionService.GetCollectionAsync(collectionId, userName);
                return Ok(collection);
            }
            catch (System.Exception)
            {
                return BadRequest(); 
            }
        }

        [HttpPost("AllCollections")]
        public async Task<IActionResult> GetUsersCollectionsAsync()
        {
            var userName = HttpContext.User.Identity.Name;

            try
            {
                var collections = await _collectionService.GetAllCollectionsAsync(userName);
                return Ok(collections);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("NewCollection")]
        public async Task<IActionResult> CreateNewCollectionAsync([FromBody] UserPostModel userPM) // bara skicka namn på collection
        {
            try
            {
                var collection = await _collectionService.NewCollectionAsync(userPM.NewCollectionName, userPM.Email);
                return Ok(collection);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}
