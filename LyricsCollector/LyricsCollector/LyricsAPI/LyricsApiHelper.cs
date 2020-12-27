using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LyricsCollector.LyricsAPI
{
    public class LyricsApiHelper
    {
        private readonly IHttpClientFactory _clientFactory;
        private LyricsModel _lyrics;

        public LyricsApiHelper(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        string errorString;

        protected async Task GetLyrics(string artist, string title)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                 "https://api.lyrics.ovh/v1/" + artist + "/" + title);

            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                _lyrics = await response.Content.ReadFromJsonAsync<LyricsModel>();
            }
            else
            {
                errorString = "Sad to say no lyrics were found";
            }
        }
    }
}
