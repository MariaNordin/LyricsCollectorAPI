using LyricsCollector.Events;
using LyricsCollector.Models.LyricsModels;
using LyricsCollector.Services.Contracts;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LyricsCollector.Services.ConcreteServices
{
    public class LyricsService : ILyricsService
    {
        private readonly IHttpClientFactory _clientFactory;
        private LyricsResponseModel _lyrics;


        public LyricsService(IHttpClientFactory clientFactory )
        {
            _clientFactory = clientFactory;
            _lyrics = new LyricsResponseModel();
        }

        public event EventHandler<LyricsEventArgs> LyricsFound;

        protected virtual void OnLyricsFound(LyricsResponseModel lyrics)
        {
            LyricsFound?.Invoke(this, new LyricsEventArgs() { Lyrics = lyrics });
        }

        public async Task<LyricsResponseModel> Search(string artist, string title)
        {

            var client = _clientFactory.CreateClient("lyrics");

            try
            {
                _lyrics = await client.GetFromJsonAsync<LyricsResponseModel>($"{artist}/{title}");
            }
            catch (Exception)
            {
                throw; //inte throw
            }

            if (_lyrics.Lyrics != "")
            {
                return _lyrics;
            }
            return null;
        }
    }
}
