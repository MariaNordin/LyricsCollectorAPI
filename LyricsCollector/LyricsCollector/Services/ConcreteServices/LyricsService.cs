using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Models.Contracts;
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
    public class LyricsService : ILyricsService, IObserver
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMemoryCache _memoryCache;
        private readonly LyricsCollectorDbContext _context;

        LyricsResponseModel lyrics = new LyricsResponseModel();

        public LyricsService(IHttpClientFactory clientFactory, LyricsCollectorDbContext context, IMemoryCache memoryCache)
        {
            _clientFactory = clientFactory;
            _memoryCache = memoryCache;
            _context = context;
        }

        //public delegate void LyricsFoundEventHandler(object source, EventArgs args);

        public event EventHandler LyricsFound;

        protected virtual void OnLyricsFound()
        {
            LyricsFound?.Invoke(this, EventArgs.Empty);
        }

        public async Task<LyricsResponseModel> Search(string artist, string title)
        {
            var dbLyrics = CheckIfLyricsInDb(artist, title);
            if (dbLyrics != null)
            {
                return dbLyrics;
            }
            else
            {
                var client = _clientFactory.CreateClient("lyrics");

                try
                {
                    lyrics = await client.GetFromJsonAsync<LyricsResponseModel>($"{artist}/{title}");
                }
                catch (Exception)
                {
                    throw;
                }

                lyrics.Artist = ToTitleCase(artist);
                lyrics.Title = ToTitleCase(title);
                await SaveLyricsToDb(lyrics);
                _memoryCache.Set("DbLyrics", _context.Lyrics.ToList());
                return lyrics;
            }
        }

        public IEnumerable<Lyrics> GetDbLyrics()
        {

            if (!_memoryCache.TryGetValue("DbLyrics", out List<Lyrics> listOfLyrics))
            {
                _memoryCache.Set("DbLyrics", _context.Lyrics.ToList());
            }

            listOfLyrics = _memoryCache.Get("DbLyrics") as List<Lyrics>;

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
        private LyricsResponseModel CheckIfLyricsInDb(string artist, string title)
        {
            artist = ToTitleCase(artist);
            title = ToTitleCase(title);

            var lyricsInDb = GetDbLyrics();
            var existingLyrics = lyricsInDb.Where(l => l.Artist == artist && l.Title == title).FirstOrDefault();

            if (existingLyrics != null)
            {
                lyrics.Artist = existingLyrics.Artist;
                lyrics.Title = existingLyrics.Title;
                lyrics.Lyrics = existingLyrics.SongLyrics;
                return lyrics;
            }

            return null;
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
                return false;
                //logg
            }

            //return new ( status: "Lyrics saved");
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

        private static string ToTitleCase(string text)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(text);
        }

        public void Update(ISubject subject)
        {
            throw new NotImplementedException();
        }
    }
}
