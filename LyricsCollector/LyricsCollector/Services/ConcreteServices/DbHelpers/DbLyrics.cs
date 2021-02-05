using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Models.LyricsModels;
using LyricsCollector.Models.LyricsModels.Contracts;
using LyricsCollector.Services.Contracts.IDbHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyricsCollector.Services.ConcreteServices.DbHelpers
{
    public class DbLyrics : IDbLyrics
    {
        private readonly LyricsCollectorDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public DbLyrics(LyricsCollectorDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task SetDbLyricsAsync()
        {
            if (!_memoryCache.TryGetValue("DbLyrics", out List<Lyrics> _))
            {
                try
                {
                    _memoryCache.Set("DbLyrics", await _context.Lyrics.ToListAsync());
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<ILyricsResponseModel> LyricsInDbMatch(string artist, string title)
        {
            await SetDbLyricsAsync();

            List<Lyrics> lyricsInDb = _memoryCache.Get("DbLyrics") as List<Lyrics>;

            var existingLyrics = lyricsInDb.Where(l => l.Artist == artist && l.Title == title).FirstOrDefault();

            var lyrics = new LyricsResponseModel();

            if (existingLyrics != null)
            {
                lyrics.Artist = existingLyrics.Artist;
                lyrics.Title = existingLyrics.Title;
                lyrics.Lyrics = existingLyrics.SongLyrics;
                lyrics.CoverImage = existingLyrics.CoverImage;
                lyrics.SpotifyLink = existingLyrics.SpotifyLink;

                return lyrics;
            }
            return null;
        }

        public async Task SaveLyricsToDbAsync(ILyricsResponseModel lyricsRM)
        {
            var lyrics = new Lyrics
            {
                Artist = lyricsRM.Artist,
                Title = lyricsRM.Title,
                SongLyrics = lyricsRM.Lyrics,
                SpotifyLink = lyricsRM.SpotifyLink,
                CoverImage = lyricsRM.CoverImage
            };

            _context.Lyrics.Add(lyrics);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }

            _memoryCache.Set("DbLyrics", await _context.Lyrics.ToListAsync());
        }
    }
}
