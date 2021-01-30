using LyricsCollector.Models.LyricsModels;
using LyricsCollector.Models.SpotifyModels;
using LyricsCollector.Models.SpotifyModels.Contracts;
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
        private ISpotifyTokenModel _token; 
        public UserResponseModel _user; 

        public SpotifyController(ISpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }


        //GET: api/Spotify
        [HttpGet("ClientCredentials")]
        public async Task<IActionResult> GetTokenAsync()
        {
            _token = await _spotifyService.GetAccessTokenAsync();

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
