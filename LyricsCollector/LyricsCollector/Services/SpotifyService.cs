using LyricsCollector.Services.Contracts;
using LyricsCollector.SpotifyClasses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LyricsCollector.Services
{
    public class SpotifyService : ISpotifyService
    {
        //private readonly IHttpClientFactory _clientFactory;

        //public SpotifyService(IHttpClientFactory clientFactory)
        //{
        //    _clientFactory = clientFactory;
        //}

        public async Task<SpotifyToken> GetAccessToken()
        {
            SpotifyToken token = new SpotifyToken();

            string postString = string.Format("grant_type=client_credentials");

            byte[] byteArray = Encoding.UTF8.GetBytes(postString);
            string url = "https://accounts.spotify.com/api/token";

            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            request.Headers.Add("Authorization", "Basic {}"); 
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);

                using WebResponse response = await request.GetResponseAsync();
                using Stream responseStream = response.GetResponseStream();
                using StreamReader reader = new StreamReader(responseStream);
                string responseFromServer = reader.ReadToEnd();
                token = JsonConvert.DeserializeObject<SpotifyToken>(responseFromServer);
            }

            return token;
        }
        //private async Task<string> GetCurrentUser()
        //{
        //    var client = _clientFactory.CreateClient("spotify");

        //    string url = "";
            
        //    WebRequest request = new RestRequest(Method.GET);
        //    request.AddHeader("Authorization", "Bearer {your access token}");
        //    IRestResponse response = client.Execute(request);
        //    Console.WriteLine(response.Content);

        //    _lyrics = await client.GetFromJsonAsync<LyricsModel>($"{artist}/{title}");
        //    message = null;
        //}
    }
}
