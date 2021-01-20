using LyricsCollector.Events;
using LyricsCollector.Models.SpotifyModels;
using System;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface ISpotifyService
    {
        public event EventHandler<TrackEventArgs> TrackFound;
        public Task<SpotifyTokenModel> GetAccessToken();
        //public Task<TrackResponseModel> GetThisTrack();
        public Task Search(string artist, string title);
    }
}
