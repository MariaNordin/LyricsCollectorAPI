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
        private UserWithToken _userWithToken;
        private ICollectionService _collectionService;

        public UserController(IUserService userService, ICollectionService collectionService)
        {
            _userService = userService;
            //_userWithToken = new UserWithToken();
            _collectionService = collectionService;
            _userService.RegisteredUser += _collectionService.OnRegisteredUser;
            _userService.UserLoggedIn += _collectionService.OnUserLoggedIn;
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
            try
            {
                _userWithToken = await _userService.Authenticate(userPM);

                if (_userWithToken != null)
                {
                    return Ok(_userWithToken);
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
