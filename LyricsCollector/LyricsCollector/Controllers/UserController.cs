using LyricsCollector.Context;
using LyricsCollector.Models;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace LyricsCollector.Controllers
{
    [Route("api/[controller]")]
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
            var user = _userService.RegisterUser(payload);
            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok(new
            {
                Status = "Registered"
            });
        }

        [HttpPost("Authenticate")]
        public IActionResult Login(UserPostModel payload)
        {
            var result = _userService.Authenticate(payload);

            if (result == null) return BadRequest(new { Message = "Username or password was incorrect." });

            return Ok(result);
        }

        //[HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public IActionResult GetUsers()
        //{
        //    var users = _userService.G
        //    return Ok();
        //}

    }
}
