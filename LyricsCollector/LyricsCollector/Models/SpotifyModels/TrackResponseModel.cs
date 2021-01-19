using LyricsCollector.Models.Contracts;
using LyricsCollector.Services.Contracts;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LyricsCollector.Models.SpotifyModels
{
    public class TrackResponseModel : ISubject
    {
        private List<IObserver> _observers;
        private Track _track;

        public TrackResponseModel()
        {
            _observers = new List<IObserver>();
        }

        [JsonPropertyName("tracks")]
        public Track Track 
        { 
            get { return _track; } 
            set
            {
                _track = value;
                Notify();
            } 
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Notify()
        {
            _observers.ForEach(o =>
            {
                o.Update(this);
            });
        }
    }


    public class Track
    {
        [JsonPropertyName("items")]
        public Item[] Items { get; set; }
    }

    public class Item
    {
        public Album Album { get; set; }
        public External_Urls External_urls { get; set; } //this
        public string Href { get; set; }
        public string Id { get; set; }
    }

    public class Album
    {
        public string Album_type { get; set; }
        public string Id { get; set; }
        public Image[] Images { get; set; } //this
        public string Name { get; set; }
        public string Release_date { get; set; }
    }

    public class Image
    {
        public string Url { get; set; }
    }

    public class External_Urls
    {
        public string Spotify { get; set; }
    }
}
