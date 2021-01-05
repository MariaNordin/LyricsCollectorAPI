using LyricsCollector.SpotifyClasses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LyricsCollector.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpotifyOAuthController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public SpotifyOAuthController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        SpotifyToken token = new SpotifyToken();

        //GET: from spotify api
        [HttpGet]
        public async Task<IActionResult> GetUsersAuthorization()
        {
            string stringResponse;

            var baseUrl = new Uri("https://accounts.spotify.com/authorize?" +
                "client_id=7e335aa2c7ed476abf4de347ae1c1ddc&response_type=code&" +
                "redirect_uri=http%3A%2F%2Flocalhost%3A3000%2F&scope=user-read-private%20user-read-email");


            using (var client = _clientFactory.CreateClient())
            {
                using (var response = await client.GetAsync(baseUrl))
                {
                    var responseContent = response.Content;
                    stringResponse = await responseContent.ReadAsStringAsync();
                }
            }

            return Ok(stringResponse);
        }

        [HttpPost]
        public async Task<IActionResult> GetSpotifyAccess()
        {
            var baseUrl = "https://accounts.spotify.com/api/token";
            return Ok();
        }

        //public IActionResult Authenticate(string user, string pwd)
        //{

        //}
    }
}
