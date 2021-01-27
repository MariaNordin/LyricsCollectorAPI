using LyricsCollector.Entities;
using LyricsCollector.Models.CollectionModels;
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
  
        [HttpPost("Collection")] 
        public async Task<IActionResult> GetCollectionAsync([FromBody] CollectionPostModel collection)
        {
            //var userName = HttpContext.User.Identity.Name;
            try
            {
                var currentCollection = await _collectionService.GetCollectionAsync(collection.Id);
                return Ok(currentCollection);
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
        public async Task<IActionResult> CreateNewCollectionAsync([FromBody] CollectionPostModel collection)
        {
            var userName = HttpContext.User.Identity.Name;

            try
            {
                var newCollection = await _collectionService.NewCollectionAsync(collection.NewName, userName);
                return Ok(collection);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveToCollectionAsync([FromBody] CollectionPostModel collection)
        {
            bool response;

            try
            {
                response = await _collectionService.SaveLyricsAsync(collection.Id);               
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

            if(response) 
                return Ok();

            return BadRequest();
        }
    }
}
