using LyricsCollector.Context;
using LyricsCollector.Models;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
        private readonly JWTSettings _jwtSettings;

        public UserController(IUserService userService, LyricsCollectorDbContext context, IOptions<JWTSettings> jwtSettings)
        {
            _userService = userService;
            _context = context;
            _jwtSettings = jwtSettings.Value;
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserPostModel userPM)
        {
            var existingUser = await _context.Users.Where(u => u.Email == userPM.Email && u.Password == userPM.Password).FirstOrDefaultAsync();

            UserWithToken userWithToken = new UserWithToken(existingUser);

            if (userWithToken == null)
            {
                return NotFound(new { Message = "Username or password was incorrect." });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, existingUser.Email)
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userWithToken.User.Token = tokenHandler.WriteToken(token);

            return Ok(userWithToken);
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
