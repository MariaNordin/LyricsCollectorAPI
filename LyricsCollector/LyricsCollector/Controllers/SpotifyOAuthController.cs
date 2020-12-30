using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LyricsCollector.Controllers
{
    public class SpotifyOAuthController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        //private LyricsModel _lyrics;

        public SpotifyOAuthController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        //GET: from spotify api

        [HttpGet]
        public async Task<IActionResult> GetUsersAuthorization()
        {
            string stringResponse;

            var baseUrl = new Uri("https://accounts.spotify.com/authorize" +
                "client_id=7e335aa2c7ed476abf4de347ae1c1ddc&response_type=code&" +
                "redirect_uri=https%3A%2F%2Flocalhost%3A44307%2Fswagger%2Findex.html&scope=user-read-private%20user-read-email");

            using (var client = _clientFactory.CreateClient())
            {
                using (var response = await client.GetAsync(baseUrl))
                {
                    var responseContent = response.Content;
                    stringResponse = await responseContent.ReadAsStringAsync();
                }
            }

            return Ok(stringResponse);
            //try
            //{
            //    _lyrics = await client.GetFromJsonAsync<LyricsModel>($"{artist}/{title}");
            //    message = null;
            //}
            //catch (Exception ex)
            //{
            //    message = $"There was an error getting the lyrics: {ex.Message}";
            //}
        }
    }
}
