using System.Text.Json.Serialization;

namespace LyricsCollector.Models.SpotifyModels
{
    public class TrackResponseModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public string Url { get; set; }
    }
}
