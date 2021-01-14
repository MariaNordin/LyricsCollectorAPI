using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Models;
using LyricsCollector.Services.Contracts;
using System;
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
        private readonly LyricsCollectorDbContext _context;

        LyricsResponseModel lyrics;

        public LyricsService(IHttpClientFactory clientFactory, LyricsCollectorDbContext context)
        {
            _clientFactory = clientFactory;
            _context = context;
        }

        public async Task<LyricsResponseModel> Search(string artist, string title)
        {
            var client = _clientFactory.CreateClient("lyrics");

            try
            {
                lyrics = await client.GetFromJsonAsync<LyricsResponseModel>($"{artist}/{title}");

                if (lyrics.Lyrics != "")
                {
                    lyrics.Artist = ToTitleCase(artist);
                    lyrics.Title = ToTitleCase(title);
                }
                return lyrics;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //public Task<string> SaveLyricsAsync(UserResponseModel userRM, LyricsResponseModel lyricsRM)
        //{
        //    var existingCollection = _context.Collections.Where(u => u.Id == userRM.CollectionId).FirstOrDefault();

        //    //var isInList = CheckLyricsInExistingList(lyricsRM, userRM.CollectionId);

        //    var lyrics = new Lyrics
        //    {
        //        Artist = lyricsRM.Artist,
        //        Title = lyricsRM.Title,
        //        Text = lyricsRM.Lyrics
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
            //string[] subs = text.Split(' ');

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(text);
        }
    }
}
