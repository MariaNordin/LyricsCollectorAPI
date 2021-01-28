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
        // PUT: Ändra uppgifter
        // DELETE: Ta bort user

        //--------------------------------------------
        private readonly IDbUserService _dbUser;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IDbUserService dbUser)
        {
            _dbUser = dbUser;
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserPostModel userPM)
        {

            var existingUser = await _dbUser.GetUser(userPM);

            if (existingUser != null)
            {
                return BadRequest();
            }

            var user = _userService.GeneratePassword(userPM);

            var result = await _dbUser.SaveUserAsync(user);
            
            if(result)
            {
                return Ok();
            }
            return BadRequest();

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserPostModel userPM)
        {
            var foundUser = await _dbUser.GetUser(userPM);
           
            if (foundUser != null)
            {
                var userWithToken = _userService.ValidatePassword(userPM, foundUser);

                if (userWithToken != null)
                {
                    return Ok(userWithToken);
                }
                return BadRequest();
            }
            else return NotFound();
        }
    }
}
