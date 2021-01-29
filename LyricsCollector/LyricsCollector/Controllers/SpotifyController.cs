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
    }
}
