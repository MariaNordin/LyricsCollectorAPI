using LyricsCollector.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LyricsCollector.Controllers
{
    public class ApiLyricsController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private LyricsModel _lyrics;

        public ApiLyricsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // GET: from open api Lyrics.ovh
        [HttpGet("{artist}/{title}")]
        public async Task<IActionResult> GetLyrics(string artist, string title)
        {
            string message;

            var client = _clientFactory.CreateClient("lyrics");

            try
            {
                _lyrics = await client.GetFromJsonAsync<LyricsModel>($"{artist}/{title}");
                message = null;
            }
            catch (Exception ex)
            {
                message = $"There was an error getting the lyrics: {ex.Message}"; //Don't need this? Always get back an empty string if no lyrics found
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


            //var request = "" + artist + "/" + title;

            //string stringResponse;

            //using (var client = _clientFactory.CreateClient())
            //{
            //    using (var response = await client.GetAsync(request))
            //    {
            //        var responseContent = response.Content;
            //        stringResponse = await responseContent.ReadAsStringAsync();
            //        //var test = JsonConvert.
            //    }
            //}
            //return Ok(stringResponse);
        }
    }
}
