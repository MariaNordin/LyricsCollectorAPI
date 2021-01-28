using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LyricsCollector.Services.ConcreteServices
{
    public class DbUser : IDbUser
    {
        private readonly LyricsCollectorDbContext _context;

        public DbUser(LyricsCollectorDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(string userName)
        {
            User user;

            try
            {
                user = await _context.Users.Where(u => u.Name == userName).FirstOrDefaultAsync();
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
                throw; //inte throw
            }

        }
    }
}
