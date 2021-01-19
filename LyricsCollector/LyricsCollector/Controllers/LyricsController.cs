using LyricsCollector.Models.LyricsModels;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [HttpPost]
        public async Task<IActionResult> GetLyrics([FromBody] LyricsResponseModel lyricsRM)
        {
            //var cacheKey = $"Get_Lyrics_From_Search-{lyricsRM}";

            try
            {
                lyrics = await _lyricsService.Search(lyricsRM.Artist, lyricsRM.Title);
            }
            catch (Exception)
            {
                return BadRequest(); // user message?
            }
            return Ok(lyrics);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
