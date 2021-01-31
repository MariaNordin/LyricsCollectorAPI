
namespace LyricsCollector.Models.SpotifyModels.Contracts
{
    public interface ITrackResponseModel
    {
        public Track Track { get; set; }
    }

    public interface ITrack
    {
        public Item[] Items { get; set; }
    }

    public interface IItem
    {
        public Album Album { get; set; }
        public External_Urls External_urls { get; set; }
        public string Href { get; set; }
        public string Id { get; set; }
    }

    //public interface Album
    //{
    //    public string Album_type { get; set; }
    //    public string Id { get; set; }
    //    public Image[] Images { get; set; }
    //    public string Name { get; set; }
    //    public string Release_date { get; set; }
    //}

    //public interface Image
    //{
    //    public string Url { get; set; }
    //}

    //public interface External_Urls
    //{
    //    public string Spotify { get; set; }
    //}

}
