using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Models.LyricsModels.Contracts;
using LyricsCollector.Services.Contracts.IDbHelpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyricsCollector.Services.ConcreteServices.DbHelpers
{
    public class DbCollections : IDbCollections
    {
        private readonly LyricsCollectorDbContext _context;

        public DbCollections(LyricsCollectorDbContext context)
        {
            _context = context;
        }

        public async Task<Collection> GetCollectionAsync(int collectionId)
        {
            Collection[] collections;

            try
            {
                collections = await _context.Collections
                   .Include(c => c.Lyrics)
                   .ThenInclude(cl => cl.Lyrics)
                   .Where(c => c.Id == collectionId)
                   .ToArrayAsync();
            }
            catch (Exception)
            {
                //logg
                throw;
            }

            if (collections.Length < 1)
            {
                return null;
            }
            return collections[0];
        }

        public async Task<List<Collection>> GetAllCollectionsAsync(string userName)
        {
            List<Collection> collections;
            try
            {
                collections = await _context.Collections
                   .Where(c => c.User.Name == userName).ToListAsync();
            }
            catch (Exception)
            {
                //logg
                throw;
            }

            return collections;
        }

        public async Task NewCollectionAsync(string CollectionName, string userName)
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
            }
            catch (Exception)
            {
                //logg
                throw;
            }
        }

        public async Task SaveLyricsAsync(int collectionId, ILyricsResponseModel currentLyrics)
        {
            var lyrics = await GetDbLyricsAsync(currentLyrics);

            var collection = await GetCurrentCollectionAsync(collectionId);

            var collectionLyrics = new CollectionLyrics();

            if (collection != null && lyrics != null) //borde kanske inte göra denna kontroll här?
            {
                collectionLyrics.Collection = collection;
                collectionLyrics.Lyrics = lyrics;

                _context.CollectionLyrics.Add(collectionLyrics);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    //logg
                    throw;
                }
            }
        }

        private async Task<User> GetUserAsync(string userName)
        {
            try
            {
                var user = await _context.Users.Where(u => u.Name == userName).FirstOrDefaultAsync();
                return user;
            }
            catch (Exception)
            {
                //logg
                throw;
            }
        }

        private async Task<Collection> GetCurrentCollectionAsync(int collectionId)
        {
            try
            {
                var currentCollection = await _context.Collections.Where(c => c.Id == collectionId).FirstOrDefaultAsync();
                return currentCollection;
            }
            catch (Exception)
            {
                //logg
                throw;
            }
        }

        private async Task<Lyrics> GetDbLyricsAsync(ILyricsResponseModel currentLyrics) 
        {
            try
            {
                var lyrics = await _context.Lyrics
                    .Where(l => l.Artist == currentLyrics.Artist 
                    && l.Title == currentLyrics.Title)
                    .FirstOrDefaultAsync();

                return lyrics;
            }
            catch (Exception)
            {
                //logg
                throw;
            }
        }
    }
}
