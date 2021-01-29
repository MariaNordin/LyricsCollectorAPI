using LyricsCollector.Models.Contracts;
using System.Text.Json.Serialization;

namespace LyricsCollector.Models.LyricsModels
{
    public class LyricsPostModel : ILyricsPostModel
    {
        [JsonPropertyName("artist")]
        public string Artist { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
