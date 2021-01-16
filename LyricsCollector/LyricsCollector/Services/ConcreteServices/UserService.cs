using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Models;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LyricsCollector.Services.ConcreteServices
{
    public class UserService : IUserService
    {
        private readonly LyricsCollectorDbContext _context;
        private readonly JWTSettings _jwtSettings;

        public UserService(LyricsCollectorDbContext context, IOptions<JWTSettings> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<ActionResult<User>> RegisterUser(UserPostModel userPM)
        {
            //User user = 
            var saltByteArray = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltByteArray);
            }

            var base64StringOfSalt = Convert.ToBase64String(saltByteArray);

            var hashedPw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userPM.Password,
                salt: saltByteArray,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
                ));

            var user = new User
            {
                Password = userPM.Password,
                Salt = base64StringOfSalt,
                Hash = hashedPw,
                Email = userPM.Email,
                Name = userPM.Name
            };

            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }
            return user;

        }

        public async Task<ActionResult<UserWithToken>> Authenticate(UserPostModel userPM)
        {
            var existingUser = await _context.Users.Where
                (u => u.Email == userPM.Email && u.Password == userPM.Password).FirstOrDefaultAsync();

            UserWithToken userWithToken = new UserWithToken(existingUser);

            if (userWithToken == null)
            {
                return null;
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

            return userWithToken;
        }
    }
}
