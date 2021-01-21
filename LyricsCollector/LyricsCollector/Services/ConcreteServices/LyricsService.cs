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
    public class LyricsService : ILyricsService, IUserWithTokenObserver
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMemoryCache _memoryCache;
        //private readonly IUserWithToken _userWithToken;
        private readonly LyricsCollectorDbContext _context;

        private User _user;
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

        public async Task<bool> SaveCollectionLyricsAsync(LyricsResponseModel lyrics, int userId, int collectionId) {

            var collectionLyrics = new CollectionLyrics();

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

        public void Notify(User user)
        {
            _user = user;
        }


        //{
        //    var existingCollection = _context.Collections.Where(c => c.CollectionOfUserId == userId).FirstOrDefault();

        //    if (existingCollectionLyrics != null)
        //    {

        //    }
        //    var existingCollectionLyrics = _context.CollectionLyrics.Where(cl => cl.CollectionId == userRM.CollectionId).FirstOrDefault();

        //    //var existingLyrics = _context.Lyrics.Where(l => l.)
        //    //var isInList = CheckLyricsInExistingList(lyricsRM, userRM.CollectionId);

        //    var lyrics = new Lyrics
        //    {
        //        Artist = lyricsRM.Artist,
        //        Title = lyricsRM.Title,
        //        SongLyrics = lyricsRM.Lyrics
        //    };

        //    if (existingCollection != null)
        //    {
        //        existingCollection.Lyrics.Add(lyrics);
        //    }
        //    return "hej";

        //}

        //private bool CheckLyricsInExistingList(LyricsResponseModel lyricsRM, int collectionId)
        //{
        //    var existingLyrics = _context.Collections.Where(c => c.Id == collectionId)
        //}
    }
}
