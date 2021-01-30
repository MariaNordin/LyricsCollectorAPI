using LyricsCollector.Models.SpotifyModels.Contracts;
using System.Text.Json.Serialization;

namespace LyricsCollector.Models.SpotifyModels
{
    public class SpotifyTokenModel : ISpotifyTokenModel
    {
        [JsonPropertyName("access_token")]
        public string Access_token { get; set; }

        [JsonPropertyName("token_type")]
        public string Token_type { get; set; }

        [JsonPropertyName("expires_in")]
        public int Expires_in { get; set; }
    }
}
