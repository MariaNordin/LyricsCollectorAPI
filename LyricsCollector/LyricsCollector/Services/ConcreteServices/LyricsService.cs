using LyricsCollector.Models.LyricsModels;
using LyricsCollector.Models.LyricsModels.Contracts;
using LyricsCollector.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LyricsCollector.Services.ConcreteServices
{
    public class LyricsService : ILyricsService
    {
        private readonly IHttpClientFactory _clientFactory;
        private List<IObserver> _observers;
        private LyricsResponseModel _lyricsResponse;

        public LyricsService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _observers = new List<IObserver>();
        }

        public async Task<ILyricsResponseModel> SearchAsync(string artist, string title)
        {

            var client = _clientFactory.CreateClient("lyrics");

            _lyricsResponse = await client.GetFromJsonAsync<LyricsResponseModel>($"{artist}/{title}");

            if (_lyricsResponse.Lyrics != "")
            {
                _lyricsResponse.Artist = artist;
                _lyricsResponse.Title = title;

                return _lyricsResponse;
            }
            return null;

        }

        public string ToTitleCase(string text)
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            return ti.ToTitleCase(text);
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Notify(ILyricsResponseModel lyrics)
        {
            foreach (var observer in _observers)
            {
                observer.Update(lyrics);
            }
        }
    }
}
