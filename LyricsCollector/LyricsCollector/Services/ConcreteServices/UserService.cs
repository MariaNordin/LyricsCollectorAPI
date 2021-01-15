using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Models;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
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

        public UserService(LyricsCollectorDbContext context)
        {
            _context = context;
        }

        public User RegisterUser(UserPostModel userPM)
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

            return new User
            {
                UserName = userPM.UserName,
                Password = userPM.Password,
                Salt = base64StringOfSalt,
                Hash = hashedPw,
                Email = userPM.Email,
                Name = userPM.Name
            };
        }

        public UserResponseModel Authenticate(UserPostModel user)
        {
            var existingUser = _context.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();

            if (existingUser == null) return null;

            var token = GenerateJwtToken(existingUser);

            return new UserResponseModel { Name = existingUser.Name, Email = existingUser.Email, Collections = existingUser.Collections };
        }

        private static string GenerateJwtToken(User existingUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("D7If38frfdFHVmRPoY68hlQl53xdMT3T"); //borde ligga i någon config

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("Username", existingUser.UserName),
                    new Claim("Group", "Admin")}),
                Expires = DateTime.Now.AddDays(7),
                Issuer = "https://localhost:44307",
                Audience = "LyricsCollector",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = tokenHandler.WriteToken(token);

            return result;
        }


    }
}
