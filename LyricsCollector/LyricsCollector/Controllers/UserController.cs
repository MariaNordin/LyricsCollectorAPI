using LyricsCollector.Entities;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Services.Contracts;
using LyricsCollector.Services.Contracts.IDbHelpers;
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
        private readonly IDbUsers _dbUser;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IDbUsers dbUser)
        {
            _userService = userService;
            _dbUser = dbUser;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserPostModel userPM)
        {
            if(String.IsNullOrWhiteSpace(userPM.Name) || String.IsNullOrWhiteSpace(userPM.Password) || String.IsNullOrWhiteSpace(userPM.Email) ) {
                return BadRequest();
            }

            var existingUser = await _dbUser.GetUserAsync(userPM.Name);

            if (existingUser != null)
            {
                return BadRequest();
            }

            var user = _userService.GeneratePassword(userPM);

            var result = await _dbUser.SaveUserAsync(user);

            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserPostModel userPM)
        {
            var foundUser = await _dbUser.GetUserAsync(userPM.Name);

            if (foundUser != null)
            {
                var userToken = _userService.ValidatePassword(userPM, foundUser);

                if (userToken != null)
                {
                    return Ok(userToken);
                }
                return BadRequest();
            }
            return NotFound();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("User")]
        public async Task<IActionResult> GetUserAsync()
        {
            User user;
            var userName = HttpContext.User.Identity.Name;

            try
            {
                user = await _dbUser.GetUserAsync(userName);               
            }
            catch (Exception)
            {
                //logg
                return BadRequest();
            }


            if (user != null)
            {
                var authenticatedUser = new UserResponseModel
                {
                    Name = user.Name,
                    Collections = user.Collections
                };
                return Ok(authenticatedUser);
            }
            return NotFound();

        }
    }
}
