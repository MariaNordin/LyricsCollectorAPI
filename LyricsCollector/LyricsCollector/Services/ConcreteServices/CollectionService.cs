using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Events;
using LyricsCollector.Models.LyricsModels;
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
        private Collection[] _collections;

        public CollectionService(LyricsCollectorDbContext context)
        {
            _context = context;
        }

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

        public async Task<IEnumerable<Collection>> GetCollectionAsync(int collectionId)
        {
            try
            {
                var collection = await _context.Collections
                   .Include(c => c.Lyrics)
                   .ThenInclude(cl => cl.Lyrics)
                   .Where(c => c.Id == collectionId
                    && c.Id == collectionId).ToArrayAsync();

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


        //public void OnLyricsFound(object source, LyricsEventArgs args)
        //{
        //    _lyrics = args.Lyrics;
        //}

        public async Task<CollectionLyrics> SaveLyricsAsync(int collectionId)
        {
            Collection collection;
            Lyrics lyrics;

            IEnumerable<Collection> collections = await GetCollectionAsync(collectionId);

            collection = collections.First();

            //ska man kolla här om collection är null?

            lyrics = await GetCurrentLyricsAsync();

            var collectionLyrics = new CollectionLyrics
            {
                Collection = collection,
                Lyrics = lyrics
            };

            _context.CollectionLyrics.Add(collectionLyrics);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return collectionLyrics;

        }

        private async Task<Lyrics> GetCurrentLyricsAsync()
        {
            try
            {
                var currentLyrics = await _context.Lyrics.OrderByDescending(l => l.Id).FirstOrDefaultAsync();
                return currentLyrics;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
