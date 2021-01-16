using LyricsCollector.Entities;
using LyricsCollector.Models;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LyricsCollector.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class LyricsController : ControllerBase
    {
        // Authorize:
        // POST: Lägga till låt i lista
        // DELETE: Ta bort låt ur lista
        // GET: Alla låtar som finns i Db (ADMIN)

        private readonly ILyricsService _lyricsService;
        private LyricsResponseModel lyrics;

        public LyricsController(ILyricsService lyricsService)
        {
            _lyricsService = lyricsService;
        }

        //POST: 
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost] // Borde väl vara get? japp och inte responseModel utan ta in string?
        public async Task<IActionResult> GetLyrics([FromBody] LyricsResponseModel lyricsRM)
        {
            //var cacheKey = $"Get_Lyrics_From_Search-{lyricsRM}";

            lyrics = await _lyricsService.Search(lyricsRM.Artist, lyricsRM.Title);

            if (lyrics is null)
            {
                //Show "Loading"
                return Ok(new { message = "Loading" });
            }
            else if (lyrics.Lyrics == "")
            {
                return Ok(new { message = "No lyrics found" });
            }
            else
            {
                //_memoryCache.Set(cacheKey, lyrics);
               
                return Ok(lyrics);
            }
        }

        [Authorize]
        [HttpPost("Save")]
        public async Task<IActionResult> SaveToCollectionAsync([FromBody] LyricsResponseModel lyricsRM, int userId, int collectionId)
        {
            var result = await _lyricsService.SaveCollectionLyricsAsync(lyricsRM, userId, collectionId);
            // Save in collection:
            // check if lyrics in db : lägg tll annars

            if (result)
            {
                return Ok(new
                {
                    Status = "Saved lyrics to list"
                });
            }
            return BadRequest(new { Status = "Saving lyrics to list failed." });

        }
    }
}
