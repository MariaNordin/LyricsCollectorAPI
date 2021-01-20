using System.Text.Json.Serialization;

namespace LyricsCollector.Models.LyricsModels
{
    public class LyricsResponseModel
    {
        [JsonPropertyName("lyrics")]
        public string Lyrics { get; set; }

        [JsonPropertyName("artist")]
        public string Artist { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        public string SpotifyLink { get; set; }
        public string CoverImage { get; set; }
    }
}
