using LyricsCollector.Models.LyricsModels;
using LyricsCollector.Models.SpotifyModels;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LyricsCollector.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class SpotifyController : ControllerBase
    {
        private readonly ISpotifyService _spotifyService;
        public SpotifyTokenModel _token; 
        public UserResponseModel _user; 

        public SpotifyController(ISpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        //GET: api/Spotify
        [HttpGet("ClientCredentials")]
        public async Task<IActionResult> GetTokenAsync()
        {
            _token = await _spotifyService.GetAccessToken();

            if (_token == null)
            {
                return BadRequest(new { response = "Getting access from Spotify failed" });
            }
            else
            {
                return Ok(_token);
            }
        }

        //[HttpPost("Search")]
        //public async Task<IActionResult> Search([FromBody] LyricsPostModel lyrics)
        //{
        //    var searchResponse = await _spotifyService.Search(lyrics.Artist, lyrics.Title);

        //    return Ok(searchResponse);
        //}


        //[HttpGet("GetThisTrack")]
        //public async Task<IActionResult> GetTrackAsync()
        //{
        //    var track = await _spotifyService.GetThisTrack();

        //    return Ok(track);
        //}



        //GET: api/Spotify (Authorization)
        //[HttpGet ("oauth")]
        //public async Task<IActionResult> GetAuthorization()
        //{
        //    var url = "https://accounts.spotify.com/authorize?" +
        //       "client_id=7e335aa2c7ed476abf4de347ae1c1ddc&" + 
        //       "response_type=code&" +
        //       "redirect_uri=http%3A%2F%2Flocalhost%3A3000%2F" +
        //       "&scope=user-read-private";

        //string authorizationResponse;

        //var client = _clientFactory.CreateClient(url);
        //return Ok();
        //client.SendAsync(url);
        //using (var client = new HttpClient())
        //{
        //    HttpResponseMessage response = await client.GetAsync(url);
        //    var responseContent = response.Content;
        //    authorizationResponse = await responseContent.ReadAsStringAsync();
        //}

        //return Ok(authorizationResponse);

        //string authorization = await _spotifyService.GetAuthorization();

        //if (authorization != null)
        //{
        //    return Ok(authorization);
        //}
        //else return BadRequest(new { error = "Something went wrong" });
        //}


        //private async Task<string> GetTokenAsync()
        //{
        //    _token = await _spotifyService.GetAccessToken();

        //    if (_token == null)
        //    {
        //        return $"Getting access from Spotify failed";
        //    }
        //    else
        //    {
        //        return _token.Access_token;
        //    }
        //}
        //[HttpGet("CurrentUser")]
        //public async Task<IActionResult> GetUser()
        //{
        //    _user = await _spotifyService.GetCurrentUser();
        //}
    }
}
