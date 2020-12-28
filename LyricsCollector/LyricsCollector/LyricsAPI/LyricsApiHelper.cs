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
            //var request = new HttpRequestMessage(HttpMethod.Get,
            //     "...link to open api..." + artist + "/" + title);

            //var client = _clientFactory.CreateClient();

            //HttpResponseMessage response = await client.SendAsync(request);

            //if (response.IsSuccessStatusCode)
            //{
            //    _lyrics = await response.Content.ReadFromJsonAsync<LyricsModel>();
            //    errorString = null;
            //}
            //else
            //{
            //    errorString = "Sad to say no lyrics were found"; 
            //}

            var client = _clientFactory.CreateClient("lyrics");

            try
            {
                _lyrics = await client.GetFromJsonAsync<LyricsModel>($"{artist}/{title}");
                errorString = null;
            }
            catch (Exception ex)
            {
                errorString = $"There was an error getting the lyrics: {ex.Message}";
            }

            
            if (string.IsNullOrWhiteSpace(errorString) == false)
            {
                //Show error message
            }
            else if (_lyrics is null)
            {
                //Show "Loading"
            }
            else
            {
                //Print data
            }
        }
    }
}
