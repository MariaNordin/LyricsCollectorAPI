using LyricsCollector.Models.LyricsModels;
using LyricsCollector.Models.SpotifyModels;
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
        private readonly ISpotifyService _spotifyService;
        private LyricsResponseModel lyrics;
        private TrackResponseModel track;

        public LyricsController(ILyricsService lyricsService, ISpotifyService spotifyService)
        {
            _lyricsService = lyricsService;
            _spotifyService = spotifyService;
        }

        //POST:
        [HttpPost("Search")]
        public async Task<IActionResult> GetLyricsAsync([FromBody] LyricsPostModel lyricsPM)
        {
            //var cacheKey = $"Get_Lyrics_From_Search-{lyricsRM}";

            try
            {
                lyrics = await _lyricsService.Search(lyricsPM.Artist, lyricsPM.Title);
                track = await _spotifyService.Search(lyricsPM.Artist, lyricsPM.Title);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            lyrics.SpotifyLink = track.Track.Items[0].External_urls.Spotify;
            lyrics.CoverImage = track.Track.Items[0].Album.Images[1].Url;
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
