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
        private User _loggedInUser;
        private Collection[] _collections;

        public CollectionService(LyricsCollectorDbContext context)
        {
            _context = context;
        }

        public void OnUserLoggedIn(object source, UserEventArgs args)
        {
            _loggedInUser = args.User;
        }

        public void OnRegisteredUser(object source, UserEventArgs args)
        {
            CreateDefaultCollection(args.User);
        }

        private void CreateDefaultCollection(User user) //async?
        {
            var collection = new Collection
            {
                Name = "MyLyrics",
                CollectionOfUserId = user.Id
            };

            _context.Collections.Add(collection);

            _context.SaveChanges();
        }

        public async Task<Collection> NewCollection(string name)
        {
            if (_loggedInUser != null)
            {
                var collection = new Collection
                {
                    Name = name,
                    CollectionOfUserId = _loggedInUser.Id
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
            else return null;
        }

        public async Task<IEnumerable<Collection>> GetCollectionAsync(int collectionId)
        {
            if (_loggedInUser != null)
            {
                try
                {
                    _collections = await _context.Collections
                       .Include(c => c.Lyrics)
                       .ThenInclude(cl => cl.Lyrics)
                       .Where(c => c.Id == collectionId
                       && c.User.Id == _loggedInUser.Id).ToArrayAsync();

                    return _collections;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else return null;
        }

        public async Task<IEnumerable<Collection>> GetAllCollectionsAsync()
        {
            if (_loggedInUser != null)
            {
                try
                {
                    _collections = await _context.Collections
                       .Include(c => c.Lyrics)
                       .ThenInclude(cl => cl.Lyrics)
                       .Where(c => c.CollectionOfUserId == _loggedInUser.Id).ToArrayAsync();

                    return _collections;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else return null;
        }
    }
}
