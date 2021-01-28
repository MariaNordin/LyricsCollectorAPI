﻿using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Models.LyricsModels;
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

        public async Task<IEnumerable<Collection>> GetCollectionAsync(int collectionId)
        {
            Collection[] collections;

            try
            {
                collections = await _context.Collections
                   .Include(c => c.Lyrics)
                   .ThenInclude(cl => cl.Lyrics)
                   .Where(c => c.Id == collectionId)
                   .ToArrayAsync();

                return collections;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Collection>> GetAllCollectionsAsync(string userName)
        {
            Collection[] collections;
            try
            {
                collections = await _context.Collections
                   .Where(c => c.User.Name == userName).ToArrayAsync();
            }
            catch (Exception)
            {
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
                throw;
            }
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

        public async Task<bool> SaveLyricsAsync(int collectionId, LyricsResponseModel currentLyrics)
        {
            var lyrics = await GetDbLyricsAsync(currentLyrics);

            var collection = await GetCurrentCollectionAsync(collectionId);

            var collectionLyrics = new CollectionLyrics();

            if (collection != null && lyrics != null)
            {
                collectionLyrics.Collection = collection;
                collectionLyrics.Lyrics = lyrics;

                _context.CollectionLyrics.Add(collectionLyrics);

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
            return false;
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

                throw;
            }
        }

        private async Task<Lyrics> GetDbLyricsAsync(LyricsResponseModel currentLyrics) 
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

                throw;
            }
        }
    }
}
