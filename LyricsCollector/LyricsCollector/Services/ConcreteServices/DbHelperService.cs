﻿using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Models.LyricsModels;
using LyricsCollector.Services.Contracts;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LyricsCollector.Services.ConcreteServices
{
    public class DbHelperService : IDbHelperService
    {
        private readonly LyricsCollectorDbContext _context;
        private readonly IMemoryCache _memoryCache;
        private LyricsResponseModel _lyrics;

        public DbHelperService(LyricsCollectorDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public LyricsResponseModel LyricsInDbMatch(string artist, string title)
        {
            artist = ToTitleCase(artist);
            title = ToTitleCase(title);

            var lyricsInDb = GetDbLyrics();

            var existingLyrics = lyricsInDb.Where(l => l.Artist == artist && l.Title == title).FirstOrDefault();

            if (existingLyrics != null)
            {
                _lyrics.Artist = existingLyrics.Artist;
                _lyrics.Title = existingLyrics.Title;
                _lyrics.Lyrics = existingLyrics.SongLyrics;
                _lyrics.CoverImage = existingLyrics.CoverImage;
                _lyrics.SpotifyLink = existingLyrics.SpotifyLink;
                return _lyrics;
            }

            return null;
        }

        private IEnumerable<Lyrics> GetDbLyrics()
        {
            if (!_memoryCache.TryGetValue("DbLyrics", out List<Lyrics> _))
            {
                _memoryCache.Set("DbLyrics", _context.Lyrics.ToList());
            }

            List<Lyrics> listOfLyrics = _memoryCache.Get("DbLyrics") as List<Lyrics>;
            return listOfLyrics;
        }

        public async Task SaveLyricsToDb(LyricsResponseModel lyricsRM)
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
                _memoryCache.Set("DbLyrics", _context.Lyrics.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string ToTitleCase(string text)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(text);
        }
    }
}