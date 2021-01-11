using LyricsCollector.Models;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LyricsCollector.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class LyricsController : ControllerBase
    {
        private readonly ILyricsService _lyricsService;
        private LyricsResponseModel lyrics;

        public LyricsController(ILyricsService lyricsService)
        {
            _lyricsService = lyricsService;
        }

        // GET: api/ApiLyrics/artist/title (from Lyrics.ovh)
        
        //POST: 
        [HttpPost()]
        public async Task<IActionResult> GetLyrics([FromBody] LyricsResponseModel lyricsRM)
        {
            lyrics = await _lyricsService.Search(lyricsRM.Artist, lyricsRM.Title);

            if (lyrics is null)
            {
                //Show "Loading"
                return Ok(new { message = "Loading" });
            }
            else if (lyrics.Lyrics == "")
            {
                return BadRequest(new { message = "No lyrics found" });
            }
            else
            {
                //Print data
                return Ok(lyrics);
            }
        }
    }
}
