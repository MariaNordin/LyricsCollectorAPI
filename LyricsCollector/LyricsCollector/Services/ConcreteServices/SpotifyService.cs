using LyricsCollector.Events;
using LyricsCollector.Models.SpotifyModels;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Services.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LyricsCollector.Services.ConcreteServices
{
    public class SpotifyService : ISpotifyService
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;
        //private readonly SpotifyCredentials _credentials;

        SpotifyTokenModel token;
        TrackResponseModel trackResponse;

        private string currentToken;

        public SpotifyService(IConfiguration config, IHttpClientFactory clientFactory)
        {
            _config = config;
            _clientFactory = clientFactory;
        }

        public async Task<TrackResponseModel> Search(string artist, string title)
        {
            var queryString = HttpUtility.UrlEncode($"{artist} {title}");

            if (currentToken == null) await GetAccessToken();

            var request = new HttpRequestMessage(HttpMethod.Get,
                $"search?q={queryString}&type=track&limit=1");
            request.Headers.Add("Authorization", $"Bearer {currentToken}");
            request.Headers.Add("Accept", "application/json");

            var client = _clientFactory.CreateClient("spotify");

            HttpResponseMessage response = await client.SendAsync(request);

            try
            {
                trackResponse = await response.Content.ReadFromJsonAsync<TrackResponseModel>();
                return trackResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<SpotifyTokenModel> GetAccessToken()
        {
            var clientId = _config.GetValue<string>("SpotifyCredentials:SpotifyClientId");  //"7e335aa2c7ed476abf4de347ae1c1ddc";
            var clientSecret = _config.GetValue<string>("SpotifyCredentials:SpotifyClientSecret");  //"1e32bdd892ad40acac7966727e3a101e";
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(
                string.Format($"{clientId}:{clientSecret}")));

            var request = new HttpRequestMessage(HttpMethod.Post,
            "https://accounts.spotify.com/api/token");
            request.Headers.Add("Authorization", $"Basic {credentials}");
            request.Headers.Add("Accept", "application/json");

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "client_credentials" }
            });

            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                token = await response.Content.ReadFromJsonAsync<SpotifyTokenModel>();
                currentToken = token.Access_token;
                return token;
            }
            else
            {
                return null;
            }
        }



        //-------------------------------------------------------

        //public async Task<string> GetAuthorization()
        //{
        //var url = "" +
        //    "client_id=7e335aa2c7ed476abf4de347ae1c1ddc&" +
        //    "response_type=code&" +
        //    "redirect_uri=https%3A%2F%2Flocalhost%3A44307%2Fswagger%2Findex.html&" +
        //    "scope=user-read-private";

        //-------------------------------------------------------
        //request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        //{
        //    { "client_id", "7e335aa2c7ed476abf4de347ae1c1ddc" },
        //    { "response_type", "code"},
        //    { "redirect_uri", "https%3A%2F%2Flocalhost%3A44307%2Fswagger%2Findex.html" },
        //    { "scope", "user-read-private" }
        //});

        //HttpResponseMessage response = await client.GetAsync(request);
        //if (response.IsSuccessStatusCode)
        //{
        //    authorization = await response.Content.ReadFromJsonAsync<AuthorizationResponseModel>
        //    return authorization;
        //}
        //else return null;
        //-------------------------------------------------------

        //using (var client = _clientFactory.CreateClient())
        //{
        //    HttpResponseMessage response = await client.GetAsync(url);
        //    var responseContent = response.Content;
        //    var authorizationResponse = await responseContent.ReadAsStringAsync();
        //}
        //}

        //public async Task<PlaylistsResponseModel> GetPlaylistsAsync(string token, string userId)
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Get,
        //        $"users/{userId}/playlists");
        //    request.Headers.Add("Authorization", $"Bearer {token}");
        //    request.Headers.Add("Accept", "application/json");

        //    var client = _clientFactory.CreateClient("spotify");

        //    HttpResponseMessage response = await client.SendAsync(request);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        _playlists = await response.Content.ReadFromJsonAsync<PlaylistsResponseModel>();
        //        return _playlists;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
