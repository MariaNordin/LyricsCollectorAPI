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

                lyrics.SpotifyLink = track.Track.Items[0].External_urls.Spotify;
                lyrics.CoverImage = track.Track.Items[0].Album.Images[1].Url;
                return Ok(lyrics);
            }
            catch (IndexOutOfRangeException ex)
            {

            }
            catch (Exception)
            {
                return BadRequest();
            }           
        }
    }
}
