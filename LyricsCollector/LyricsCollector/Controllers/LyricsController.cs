using LyricsCollector.Models;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace LyricsCollector.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class LyricsController : ControllerBase
    {
        // POST: Lägga till låt i lista
        // DELETE: Ta bort låt ur lista

        private readonly ILyricsService _lyricsService;
        private readonly IMemoryCache _memoryCache;
        private LyricsResponseModel lyrics;

        public LyricsController(ILyricsService lyricsService, IMemoryCache memoryCache)
        {
            _lyricsService = lyricsService;
            _memoryCache = memoryCache;
        }

        //POST: 
        [HttpPost]
        public async Task<IActionResult> GetLyrics([FromBody] LyricsResponseModel lyricsRM)
        {
            var cacheKey = $"Get_Lyrics_From_Search-{lyricsRM}";

            if (_memoryCache.TryGetValue(cacheKey, out string cachedValue))
                return Ok(cachedValue);

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
                _memoryCache.Set(cacheKey, lyrics);
                return Ok(lyrics);
            }
        }
        //[HttpPost("Save")]
        //public async Task<IActionResult> SaveLyrics([FromBody] LyricsResponseModel lyricsRM, UserResponseModel userRM)
        //{
        //    var existingCollection = _context.Collections.Where(u => u.Id == userRM.CollectionId).FirstOrDefault();

        //    return Ok();
        //}
    }
}
