using LyricsCollector.Events;
using LyricsCollector.Models.SpotifyModels;
using System;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface ISpotifyService
    {
        //public event EventHandler<LyricsEventArgs> TrackFound;
        public Task<SpotifyTokenModel> GetAccessToken();
        //public Task<TrackResponseModel> GetThisTrack();
        public Task<TrackResponseModel> Search(string artist, string title);
    }
}
