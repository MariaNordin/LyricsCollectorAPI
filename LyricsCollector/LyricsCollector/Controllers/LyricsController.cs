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
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILyricsService _lyricsService;
        private LyricsResponseModel _lyrics;

        public LyricsController(IHttpClientFactory clientFactory, ILyricsService lyricsService)
        {
            _clientFactory = clientFactory;
            _lyricsService = lyricsService;
        }

        // GET: api/ApiLyrics/artist/title (from Lyrics.ovh)
        [HttpPost("{artist}/{title}")]
        public async Task<IActionResult> GetLyrics([FromBody] LyricsResponseModel lyricsRM)
        {
            string message;

            var client = _clientFactory.CreateClient("lyrics");

            try
            {
                _lyrics = await client.GetFromJsonAsync<LyricsResponseModel>($"{lyricsRM.Artist}/{lyricsRM.Title}");
                message = null;
            }
            catch (Exception ex)
            {
                message = $"There was an error getting the lyrics: {ex.Message}"; //Don't need this? Always gets back an empty string if no lyrics found
            }


            if (string.IsNullOrWhiteSpace(message) == false) //And don-t need following either, eccept for else - print data
            {
                //Show error message
                return BadRequest(message); 
            }
            else if (_lyrics is null)
            {
                //Show "Loading"
                return Ok(new { message = "Loading" });
            }
            else if (_lyrics.Lyrics == "")
            {
                return BadRequest( new { message = "No lyrics found" });
            }
            else
            {
                //Print data
                return Ok(_lyrics);
            }
        }
    }
}
