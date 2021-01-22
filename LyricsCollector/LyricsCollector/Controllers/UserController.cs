using LyricsCollector.Models.UserModels;
using LyricsCollector.Services.ConcreteServices;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LyricsCollector.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CORSPolicy")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: Hämta alla users
        // GET: Hämta en specifik user
        // POST: "Register" (ny user)
        // PUT: Ändra uppgifter
        // DELETE: Ta bort user

        //--------------------------------------------
        private readonly IUserService _userService;
        //private UserWithToken _userWithToken;
        //private ICollectionService _collectionService;

        public UserController(IUserService userService)
        {
            _userService = userService;
            //_collectionService = collectionService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserPostModel payload)
        {
            try
            {
                var result = await _userService.RegisterUser(payload);
            }
            catch (Exception ex)
            {
                // logg
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserPostModel userPM)
        {
            try
            {
                var authenticatedUser = await _userService.Authenticate(userPM);

                if (authenticatedUser != null)
                {
                    return Ok(authenticatedUser);
                }
                return Unauthorized();

            }
            catch (Exception ex)
            {
                // logg
                return BadRequest(ex.Message);
            }
        }
    }
}
