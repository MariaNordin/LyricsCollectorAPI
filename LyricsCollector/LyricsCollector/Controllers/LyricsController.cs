using LyricsCollector.Entities;
using LyricsCollector.Models.LyricsModels;
using LyricsCollector.Models.SpotifyModels;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LyricsCollector.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class LyricsController : ControllerBase
    {
        // GET: Alla låtar som finns i Db (Authorize: ADMIN)
        // POST: Search

        private readonly IDbHelperService _dbHelper;
        private readonly ILyricsService _lyricsService;
        private readonly ISpotifyService _spotifyService;
        private LyricsResponseModel lyrics;
        private TrackResponseModel track;

        public LyricsController(IDbHelperService dbHelper, ILyricsService lyricsService, ISpotifyService spotifyService)
        {
            _lyricsService = lyricsService;
            _spotifyService = spotifyService;
            _dbHelper = dbHelper;
        }

        [HttpPost("Search")]
        public async Task<IActionResult> GetLyrics([FromBody] LyricsPostModel lyricsPM)
        {
            //var cacheKey = $"Get_Lyrics_From_Search-{lyricsRM}";

            var dbLyrics = _dbHelper.LyricsInDbMatch(lyricsPM.Artist, lyricsPM.Title);
            
            if (dbLyrics != null)
            {
                return Ok(dbLyrics);
            }

            try
            {
                lyrics = await _lyricsService.Search(lyricsPM.Artist, lyricsPM.Title);
                track = await _spotifyService.Search(lyricsPM.Artist, lyricsPM.Title);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            if (lyrics != null)
            {
                lyrics.SpotifyLink = track.Track.Items[0].External_urls.Spotify;
                lyrics.CoverImage = track.Track.Items[0].Album.Images[1].Url;
                await _dbHelper.SaveLyricsToDb(lyrics);
                return Ok(lyrics);
            }
            return BadRequest();
        }






    }
}
