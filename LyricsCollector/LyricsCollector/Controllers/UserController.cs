using LyricsCollector.Models.UserModels;
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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserPostModel payload)
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
            UserWithToken userWithToken;

            try
            {
                userWithToken = await _userService.Authenticate(userPM);

                if (userWithToken != null)
                {
                    return Ok(userWithToken);
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                // logg
                return BadRequest(ex.Message);
            }
        }
    }
}
