using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Models.LyricsModels;
using LyricsCollector.Services.Contracts;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LyricsCollector.Services.ConcreteServices
{
    public class LyricsService : ILyricsService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMemoryCache _memoryCache;
        private readonly LyricsCollectorDbContext _context;

        private LyricsResponseModel _lyrics = new LyricsResponseModel();

        public LyricsService(IHttpClientFactory clientFactory, LyricsCollectorDbContext context, IMemoryCache memoryCache)
        {
            _clientFactory = clientFactory;
            _memoryCache = memoryCache;
            _context = context;
        }

        public async Task<LyricsResponseModel> Search(string artist, string title)
        {
            var dbLyrics = LyricsInDbMatch(artist, title);
            if (dbLyrics != null)
            {
                return dbLyrics;
            }
            else
            {
                var client = _clientFactory.CreateClient("lyrics");

                try
                {
                    _lyrics = await client.GetFromJsonAsync<LyricsResponseModel>($"{artist}/{title}");
                }
                catch (Exception)
                {
                    throw;
                }

                _lyrics.Artist = ToTitleCase(artist);
                _lyrics.Title = ToTitleCase(title);

                if (_lyrics.Lyrics != "")
                {
                    await SaveLyricsToDb(_lyrics);
                    _memoryCache.Set("DbLyrics", _context.Lyrics.ToList());
                }              
                return _lyrics;
            }
        }

        public IEnumerable<Lyrics> GetDbLyrics()
        {
            if (!_memoryCache.TryGetValue("DbLyrics", out List<Lyrics> _))
            {
                _memoryCache.Set("DbLyrics", _context.Lyrics.ToList());
            }

            List<Lyrics> listOfLyrics = _memoryCache.Get("DbLyrics") as List<Lyrics>;
            return listOfLyrics;
        }

        private async Task SaveLyricsToDb(LyricsResponseModel lyricsRM)
        {
            var lyrics = new Lyrics
            {
                Artist = lyricsRM.Artist,
                Title = lyricsRM.Title,
                SongLyrics = lyricsRM.Lyrics
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
        }
        private LyricsResponseModel LyricsInDbMatch(string artist, string title)
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
                return _lyrics;
            }

            return null;
        }
        private static string ToTitleCase(string text)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(text);
        }
    }
}
