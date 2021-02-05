using LyricsCollector.Entities;
using LyricsCollector.Models.CollectionModels;
using LyricsCollector.Services.Contracts;
using LyricsCollector.Services.Contracts.IDbHelpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            Collection currentCollection;

            try
            {
                currentCollection = await _dbHelper.GetCollectionAsync(collection.Id);              
            }
            catch (Exception) //lägg till fler specifika ex?
            {
                //logg?
                return BadRequest(); //meddelande
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

            List<Collection> collections;
            try
            {
                collections = await _dbHelper.GetAllCollectionsAsync(userName);
            }
            catch (Exception) //lägg till fler specifika ex?
            {
                return BadRequest(); //meddelande
            }

            if (collections != null)
            {
                return Ok(collections);
            }
            return NotFound(new { message = "No collections found." }); //meddelande

        }

        [HttpPost("NewCollection")]
        public async Task<IActionResult> CreateNewCollectionAsync([FromBody] CollectionPostModel collection)
        {
            var userName = HttpContext.User.Identity.Name;

            try
            {
                await _dbHelper.NewCollectionAsync(collection.NewName, userName);
                return Ok(new { message = "Collection successfully created!" }); //Meddelande
            }
            catch (Exception) //lägg till fler specifika ex?
            {
                return BadRequest(); //meddelande
            }       
        }

        [HttpPost("SaveLyrics")]
        public async Task<IActionResult> SaveToCollectionAsync([FromBody] CollectionPostModel collection)
        {
            var lyrics = _collectionService.GetCurrentLyrics();

            try
            {
                await _dbHelper.SaveLyricsAsync(collection.Id, lyrics);
                return Ok(new { message = "Lyrics saved!" });
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException.Message.Contains("PRIMARY KEY constraint"))
                {
                    return BadRequest(new { message = "This song is already in your collection." });
                }
                else return BadRequest();
                //else: log e.InnerException.Message
            }
            catch (Exception) //lägg till fler specifika ex? + spec meddelanden
            {
                return BadRequest(new { message = "Something went wrong. Please try again" });
            }           
        }
    }
}
