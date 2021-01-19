using System.Text.Json.Serialization;

namespace LyricsCollector.Models.LyricsModels
{
    public class LyricsPostModel
    {
        [JsonPropertyName("artist")]
        public string Artist { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
