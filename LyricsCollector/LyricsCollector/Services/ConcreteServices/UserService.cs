﻿using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Models;
using LyricsCollector.Services.Contracts;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LyricsCollector.Services.ConcreteServices
{
    public class UserService : IUserService
    {
        private readonly LyricsCollectorDbContext _context;

        public UserService(LyricsCollectorDbContext context)
        {
            _context = context;
        }

        public AuthenticateResponse Authenticate(UserPostModel user)
        {
            var existingUser = _context.Users.Where(u => u.UserName == user.UserName).FirstOrDefault();

            if (existingUser == null) return null;

            var token = GenerateJwtToken(existingUser);

            return new AuthenticateResponse(existingUser, token);
        }

        private static string GenerateJwtToken(User existingUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("asjdkflö"); //borde ligga i någon config

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { new Claim("Username", existingUser.UserName), new Claim("Group", "Admin")}),
                Expires = DateTime.Now.AddDays(7),
                Issuer = "https://localhost:5001",
                Audience = "Nån app",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = tokenHandler.WriteToken(token);

            return result;
        }

        public User RegisterUser(UserPostModel userPM)
        {
            var saltByteArray = new byte[128/8];

            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltByteArray);
            }

            var base64StringOfSalt = Convert.ToBase64String(saltByteArray);
            //var base64StringOfSaltHashcode = Convert.ToBase64String(saltByteArray).GetHashCode();

            var hashedPw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userPM.Password,
                salt: saltByteArray,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 100000,
                numBytesRequested: 256/8
                ));

            return new User { UserName = userPM.UserName, Salt = base64StringOfSalt, Hash = hashedPw, Email = userPM.Email, Name = userPM.Name};
        }
    }
}