using LyricsCollector.Models.LyricsModels;
using LyricsCollector.Models.SpotifyModels;
using LyricsCollector.Services.Contracts;
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
        IDbLyrics _dbLyricsHelper;
        private readonly ILyricsService _lyricsService;
        private readonly ISpotifyService _spotifyService;
        private LyricsResponseModel lyrics;
        private TrackResponseModel track;

        public LyricsController(ILyricsService lyricsService, ISpotifyService spotifyService, IDbLyrics dbLyrics)
        {
            _lyricsService = lyricsService;
            _spotifyService = spotifyService;
            _dbLyricsHelper = dbLyrics;
        }

        //POST:
        [HttpPost("Search")]
        public async Task<IActionResult> GetLyricsAsync([FromBody] LyricsPostModel lyricsPM)
        {
            //var cacheKey = $"Get_Lyrics_From_Search-{lyricsRM}";

            var dbLyrics = _dbLyricsHelper.LyricsInDbMatch(lyricsPM.Artist, lyricsPM.Title);

            if (dbLyrics != null)
            {
                return Ok(dbLyrics);
            }

            lyrics = await _lyricsService.Search(lyricsPM.Artist, lyricsPM.Title);

            if (lyrics != null)
            {
                try
                {
                    track = await _spotifyService.Search(lyricsPM.Artist, lyricsPM.Title);

                    lyrics.SpotifyLink = track.Track.Items[0].External_urls.Spotify;
                    lyrics.CoverImage = track.Track.Items[0].Album.Images[1].Url;
                }
                catch (IndexOutOfRangeException)
                {
                    return NotFound();
                }
                catch (Exception)
                {
                    return BadRequest();
                }

                await _dbLyricsHelper.SaveLyricsToDbAsync(lyrics);
                return Ok(lyrics);
            }
            return NotFound();
        }
    }
}
