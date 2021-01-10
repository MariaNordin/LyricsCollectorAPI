using System.Text.Json.Serialization;

namespace LyricsCollector.Models
{
    public class LyricsResponseModel
    {
        [JsonPropertyName("lyrics")]
        public string Lyrics { get; set; }
    }
}
