using LyricsCollector.Entities;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Services.ConcreteServices;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> RegisterAsync([FromBody] UserPostModel payload)
        {
            User user;

            try
            {
                user = await _userService.RegisterUserAsync(payload);
            }
            catch (Exception ex)
            {
                // logg
                return BadRequest(ex.Message);
            }

            if (user == null)
            {
                return Ok(new { message = "Username or Email allready exists." });
            }

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserPostModel userPM)
        {
            try
            {
                var userToken = await _userService.AuthenticateAsync(userPM);

                if (userToken != null)
                {
                    return Ok(userToken);
                }
                return Unauthorized();

            }
            catch (Exception ex)
            {
                // logg
                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("User")]
        public async Task<IActionResult> GetUserAsync()
        {
            var userName = HttpContext.User.Identity.Name;

            try
            {
                var user = await _userService.GetUserAsync(userName);
                return Ok(user);
            }
            catch (Exception)
            {
                //logg
                return BadRequest();
            }
        }
    }
}
