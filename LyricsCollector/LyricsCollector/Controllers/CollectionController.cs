using LyricsCollector.Entities;
using LyricsCollector.Models.CollectionModels;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Services.Contracts;
using LyricsCollector.Services.Contracts.IDbHelpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        private readonly IDbCollections _dbHelper;
        private readonly ICollectionService _collectionService;

        public CollectionController(IDbCollections dbHelper, ICollectionService collectionService)
        {
            _collectionService = collectionService;
            _dbHelper = dbHelper;
        }
  
        [HttpPost("Collection")] 
        public async Task<IActionResult> GetCollectionAsync([FromBody] CollectionPostModel collection)
        {
            IEnumerable<Collection> currentCollection;

            try
            {
                currentCollection = await _dbHelper.GetCollectionAsync(collection.Id);              
            }
            catch (System.Exception)
            {
                return BadRequest(); 
            }

            if (currentCollection != null)
            {
                return Ok(currentCollection);
            }
            return NotFound();
        }

        [HttpPost("AllCollections")]
        public async Task<IActionResult> GetUsersCollectionsAsync()
        {
            var userName = HttpContext.User.Identity.Name;

            try
            {
                var collections = await _dbHelper.GetAllCollectionsAsync(userName);
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
                await _dbHelper.NewCollectionAsync(collection.NewName, userName);
                
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("SaveLyrics")]
        public async Task<IActionResult> SaveToCollectionAsync([FromBody] CollectionPostModel collection)
        {
            bool response;

            var lyrics = _collectionService.GetCurrentLyrics();

            try
            {
                response = await _dbHelper.SaveLyricsAsync(collection.Id, lyrics);               
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
