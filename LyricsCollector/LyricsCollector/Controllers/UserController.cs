using LyricsCollector.Context;
using LyricsCollector.Models;
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
        private readonly LyricsCollectorDbContext _context;


        public UserController(IUserService userService, LyricsCollectorDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserPostModel payload)
        {
            var user = await _userService.RegisterUser(payload);

            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserPostModel userPM)
        {
            var userWithToken = await _userService.Authenticate(userPM);

            if (userWithToken == null)
            {
                return NotFound(new { Message = "Username or password was incorrect." });
            }
            return Ok(userWithToken);
        }
    }
}
