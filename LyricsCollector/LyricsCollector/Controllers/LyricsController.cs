using LyricsCollector.Models.LyricsModels;
using LyricsCollector.Models.LyricsModels.Contracts;
using LyricsCollector.Services.Contracts;
using LyricsCollector.Services.Contracts.IDbHelpers;
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
        private readonly IDbLyrics _dbLyricsHelper;
        private readonly ILyricsService _lyricsService;
        private readonly ISpotifyService _spotifyService;
        private ILyricsResponseModel _lyrics;

        public LyricsController (ILyricsService lyricsService, ISpotifyService spotifyService, 
            IDbLyrics dbLyrics, ICollectionService collectionService)
        {
            _lyricsService = lyricsService;
            _spotifyService = spotifyService;
            _dbLyricsHelper = dbLyrics;

            _lyricsService.Attach(collectionService);
        }

        //POST:
        [HttpPost("Search")]
        public async Task<IActionResult> GetLyricsAsync([FromBody] LyricsPostModel lyricsPM)
        {
            var artist = _lyricsService.ToTitleCase(lyricsPM.Artist);
            var title = _lyricsService.ToTitleCase(lyricsPM.Title);

            try
            {
                _lyrics = await _dbLyricsHelper.LyricsInDbMatch(artist, title);
            }
            catch (Exception)
            {
                //logg
                return BadRequest(new { message = "Something went wrong" });
            }
            

            if (_lyrics != null)
            {
                _lyricsService.Notify((ILyricsResponseModel)_lyrics);
                return Ok(_lyrics);
            }

            try
            {
                _lyrics = await _lyricsService.SearchAsync(artist, title);
            }
            catch (Exception)
            {
                //logg
                return BadRequest(new { message = "Something went wrong" }); //beroende på fel borde appl. stoppas?
            }

            if (_lyrics != null)
            {
                try
                {
                    var track = await _spotifyService.SearchAsync(artist, title);

                    _lyrics.SpotifyLink = track.Track.Items[0].External_urls.Spotify;
                    _lyrics.CoverImage = track.Track.Items[0].Album.Images[0].Url;
                }
                catch (IndexOutOfRangeException)
                {
                    return NotFound(new { message = "No lyrics found" }); //Meddelande, användaren har skickat in konstiga värden
                }
                catch (Exception)
                {
                    //logg
                    return BadRequest(new { message = "Something went wrong" } ); //Meddelande
                }


                //try
                await _dbLyricsHelper.SaveLyricsToDbAsync(_lyrics);
                //catch

                _lyricsService.Notify(_lyrics);

                return Ok(_lyrics);
            }
            return NotFound(); //meddelande - user har skickat in konstiga värden/lyrics finns inte till låten
        }
    }
}
