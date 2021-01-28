using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Models;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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
        private readonly JWTSettings _jwtSettings;

        public UserService(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public User GeneratePassword(UserPostModel userPM)
        {
            var saltByteArray = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltByteArray);
            }

            var hashedPw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userPM.Password,
                salt: saltByteArray,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
                ));

            var user = new User
            {
                Salt = saltByteArray,
                Hash = hashedPw,
                Email = userPM.Email,
                Name = userPM.Name
            };
            return user;
        }

        public UserWithToken ValidatePassword(UserPostModel userPM, User user)
        {
            var hashedPw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userPM.Password,
                salt: user.Salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
                ));

            if (hashedPw == user.Hash)
            {
                var token = GenerateJwtToken(user);

                var userWithToken = new UserWithToken
                {
                    User = user,
                    Token = token
                };
                return userWithToken;
            }
            return null;
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email)
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = tokenHandler.WriteToken(token);

            return result;
        }
    }
}
