using LyricsCollector.Entities.Contracts;
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
        private IUser _user;

        public UserController(IUserService userService, IDbUsers dbUser, IUser user)
        {
            _userService = userService;
            _dbUser = dbUser;
            _user = user;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserPostModel userPM)
        {
            if(String.IsNullOrWhiteSpace(userPM.Name) || String.IsNullOrWhiteSpace(userPM.Password) || String.IsNullOrWhiteSpace(userPM.Email) ) {
                return BadRequest();
            }

            _user = await _dbUser.GetUserAsync(userPM.Name);

            if (_user != null)
            {
                return BadRequest();
            }

            _user = _userService.GeneratePassword(userPM);

            var result = await _dbUser.SaveUserAsync(_user);

            if (result)
            {
                return Ok(new { message = "Registered!" } );
            }
            return BadRequest(new { message = "Register failed. Try again" } );
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserPostModel userPM)
        {
            _user = await _dbUser.GetUserAsync(userPM.Name);

            if (_user != null)
            {
                var userToken = _userService.ValidatePassword(userPM, _user);

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
            
            var userName = HttpContext.User.Identity.Name;

            try
            {
               _user = await _dbUser.GetUserAsync(userName);               
            }
            catch (Exception)
            {
                //logg
                return BadRequest();
            }

            if (_user != null)
            {
                var authenticatedUser = new UserResponseModel
                {
                    Name = _user.Name,
                    Collections = _user.Collections
                };
                return Ok(authenticatedUser);
            }
            return NotFound();
        }
    }
}
