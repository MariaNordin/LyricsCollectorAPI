using LyricsCollector.Models;
using LyricsCollector.Services.Contracts;
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

        LyricsResponseModel lyrics;

        public LyricsService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
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

        private static string ToTitleCase(string text)
        {
            //string[] subs = text.Split(' ');

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(text);
        }
    }
}
