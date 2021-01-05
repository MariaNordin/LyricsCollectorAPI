using LyricsCollector.Services.Contracts;
using LyricsCollector.SpotifyClasses;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LyricsCollector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyController : ControllerBase
    {
        private readonly ISpotifyService _spotifyService;
        private SpotifyToken _token;

        public SpotifyController(ISpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        //GET: token
        [HttpGet]
        public async Task<IActionResult> GetToken()
        {
            _token = await _spotifyService.GetAccessToken();

            return Ok(_token);
        }

        //GET: from spotify api
        //[HttpGet]
        //public async Task<IActionResult> GetUsersAuthorization()
        //{
        //    string stringResponse;


        //    var baseUrl = new Uri("" +
        //        "client_id=7e335aa2c7ed476abf4de347ae1c1ddc&response_type=code&" +
        //        "redirect_uri=http%3A%2F%2Flocalhost%3A3000%2F&scope=user-read-private%20user-read-email");


        //    var ClientId = "7e335aa2c7ed476abf4de347ae1c1ddc2";
        //    var ClientSecret = "1e32bdd892ad40acac7966727e3a101e";

        //    var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", ClientId, ClientSecret)));

        //    var client1 = new RestClient("");
        //    client1.Timeout = -1;
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("Authorization", $"Basic {token}");
        //    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        //    request.AddParameter("grant_type", "authorization_code");
        //    request.AddParameter("code", "MQCbtKe...44KN");
        //    request.AddParameter("redirect_uri", "");
        //    IRestResponse response = client1.Execute(request);
        //    Console.WriteLine(response.Content);

        //    using (var client = _clientFactory.CreateClient())
        //    {
        //        client..Headers.Add("Authorization", "Basic " + token);

        //        using (var response = await client.GetAsync(baseUrl))
        //        {
        //            var responseContent = response.Content;
        //            stringResponse = await responseContent.ReadAsStringAsync();
        //        }
        //    }

        //    return Ok(stringResponse);
        //}
    }
}
