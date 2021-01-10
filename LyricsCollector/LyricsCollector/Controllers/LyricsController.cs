using LyricsCollector.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LyricsCollector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LyricsController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private LyricsResponseModel _lyrics;

        public LyricsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // GET: api/ApiLyrics/artist/title (from Lyrics.ovh)
        [HttpGet("{artist}/{title}")]
        public async Task<IActionResult> GetLyrics(string artist, string title)
        {
            string message;

            var client = _clientFactory.CreateClient("lyrics");

            try
            {
                _lyrics = await client.GetFromJsonAsync<LyricsResponseModel>($"{artist}/{title}");
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
            else
            {
                //Print data
                return Ok(_lyrics);
            }
        }
    }
}
