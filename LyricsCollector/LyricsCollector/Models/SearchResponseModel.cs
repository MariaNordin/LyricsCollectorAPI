using System.Text.Json.Serialization;

namespace LyricsCollector.Models
{
    public class SearchResponseModel
    {
        [JsonPropertyName("external_urls")]
        public string External_urls { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class Item
    {
        [JsonPropertyName("external_urls")]
        public string External_urls { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

    }

    public class External_Urls
    {
        [JsonPropertyName("spotify")]
        public string Spotify { get; set; }
    }

    //[JsonPropertyName("items")]
    //public Item[] Items { get; set; }
}

