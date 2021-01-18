using System.Text.Json.Serialization;

namespace LyricsCollector.Models
{
    public class SearchResponseModel
    {
        [JsonPropertyName("tracks")]
        public Track Track { get; set; }
    }


    public class Track
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("items")]
        public Item[] Items { get; set; }
    }

    public class Item
    {
        public Album Album { get; set; }

        [JsonPropertyName("artists")]
        public Artist[] Artists { get; set; }
        public External_Urls External_urls { get; set; }
        public string Href { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public int Popularity { get; set; }
    }

    public class Album
    {
        public string Album_type { get; set; }
        public string Id { get; set; }
        public Image[] Images { get; set; }
        public string Name { get; set; }
        public string Release_date { get; set; }

    }

    public class Image
    {
        public int Height { get; set; }
        public string Url { get; set; }
        public int Width { get; set; }
    }

    public class External_Urls
    {
        public string Spotify { get; set; }
    }

    public class Artist
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
