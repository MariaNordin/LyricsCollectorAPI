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
        private readonly LyricsCollectorDbContext _context;
        private readonly JWTSettings _jwtSettings;

        public UserService(LyricsCollectorDbContext context, IOptions<JWTSettings> jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<User> RegisterUser(UserPostModel userPM)
        {
            var existingUser = await _context.Users.Where(u => u.Email == userPM.Email).FirstOrDefaultAsync();


            if (existingUser != null)
            {
                return existingUser;
            }
            else
            {
                
                var user = GeneratePassword(userPM);
                _context.Users.Add(user);

                try
                {
                    await _context.SaveChangesAsync();
                    return user; 
                }
                catch (Exception)
                {
                    throw;
                }
                
            }
        }

        private static User GeneratePassword(UserPostModel userPM)
        {
            var saltByteArray = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltByteArray);
            }

            //var base64StringOfSalt = Convert.ToBase64String(saltByteArray);

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

        public async Task<UserWithToken> Authenticate(UserPostModel userPM)
        {
            var existingUser = await ValidatePassword(userPM);

            if (existingUser == null) return null;

            var token = GenerateJwtToken(existingUser);

            //var authenticatedUser = new User
            //{
            //    Email = existingUser.Email,
            //    Name = existingUser.Name,
            //    Collections = existingUser.Collections
            //};

            return new UserWithToken(existingUser, token);
        }

        private async Task<User> ValidatePassword(UserPostModel userPM)
        {
            var foundUser = await _context.Users.Where
                (u => u.Email == userPM.Email).FirstOrDefaultAsync();

            var hashedPw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userPM.Password,
                salt: foundUser.Salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
                ));

            if (hashedPw == foundUser.Hash) return foundUser;
            else return null;
        }

        private string GenerateJwtToken(User existingUser)
        {
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
            var result = tokenHandler.WriteToken(token);

            return result;
        }
    }
}
