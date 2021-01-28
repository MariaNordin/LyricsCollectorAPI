using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyricsCollector.Services.ConcreteServices
{
    public class DbUserService : IDbUserService
    {
        private readonly LyricsCollectorDbContext _context;

        public DbUserService(LyricsCollectorDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(UserPostModel userPM)
        {
            var user = await _context.Users.Where(u => u.Email == userPM.Email).FirstOrDefaultAsync();

            if (user != null)
            {
                return user;
            }
            return null;
        }

        public async Task<bool> SaveUserAsync(User user)
        {
            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
