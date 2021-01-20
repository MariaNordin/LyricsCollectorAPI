using LyricsCollector.Models.SpotifyModels;
using System;

namespace LyricsCollector.Events
{
    public class TrackEventArgs : EventArgs
    {
        public TrackResponseModel Track { get; set; }
    }
}
