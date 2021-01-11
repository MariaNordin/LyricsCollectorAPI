using System.Text.Json.Serialization;

namespace LyricsCollector.Models
{
    public class LyricsResponseModel
    {
        [JsonPropertyName("lyrics")]
        public string Lyrics { get; set; }

        [JsonPropertyName("artist")]
        public string Artist { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
