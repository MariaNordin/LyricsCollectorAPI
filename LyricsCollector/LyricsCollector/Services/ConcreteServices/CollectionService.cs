using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Events;
using LyricsCollector.Models.CollectionModels;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyricsCollector.Services.ConcreteServices
{
    public class CollectionService : ICollectionService
    {
        private readonly LyricsCollectorDbContext _context;
        //private UserService _userService;
        //private User _loggedInUser;
        private Collection[] _collections;

        public CollectionService(LyricsCollectorDbContext context)
        {
            _context = context;
        }

        //public void OnUserLoggedIn(object source, UserEventArgs args)
        //{
        //    _loggedInUser = args.User;
        //}

        //public void OnRegisteredUser(object source, UserEventArgs args)
        //{
        //    CreateDefaultCollection(args.User);
        //}

        private async Task<User> GetUserAsync(string userName)
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

        public async Task<Collection> NewCollectionAsync(string CollectionName, string userName)
        {
            var user = await GetUserAsync(userName);

            var collection = new Collection
            {
                Name = CollectionName,
                User = user
            };

            _context.Collections.Add(collection);

            try
            {
                await _context.SaveChangesAsync();
                return collection;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Collection>> GetCollectionAsync(int collectionId, string userName)
        {
            try
            {
                var collection = await _context.Collections
                   .Include(c => c.Lyrics)
                   .ThenInclude(cl => cl.Lyrics)
                   .Where(c => c.Id == collectionId
                   && c.User.Name == userName && c.Id == collectionId).ToArrayAsync();

                return collection;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Collection>> GetAllCollectionsAsync(string userName)
        {
            try
            {
                _collections = await _context.Collections
                   .Include(c => c.Lyrics)
                   .ThenInclude(cl => cl.Lyrics)
                   .Where(c => c.User.Name == userName).ToArrayAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return _collections;
        }
    }
}
