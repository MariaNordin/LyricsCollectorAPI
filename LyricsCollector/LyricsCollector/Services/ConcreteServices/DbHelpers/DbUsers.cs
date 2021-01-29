using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Services.Contracts.IDbHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LyricsCollector.Services.ConcreteServices.DbHelpers
{
    public class DbUsers : IDbUsers
    {
        private readonly LyricsCollectorDbContext _context;

        public DbUsers(LyricsCollectorDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(string userName)
        {
            User user;

            try
            {
                user = await _context.Users
                    .Include(u => u.Collections)
                    .ThenInclude(c => c.Lyrics)
                    .ThenInclude(cl => cl.Lyrics)
                    .Where(u => u.Name == userName).FirstOrDefaultAsync();

                return user;
            }
            catch (Exception)
            {

                throw;
            }
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
