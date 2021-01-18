using LyricsCollector.Models;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface ISpotifyService
    {
        public Task<SpotifyTokenModel> GetAccessToken();
        public Task<TrackResponseModel> GetThisTrack();
        public Task<Image> Search(string artist, string title);
        public Task<UserResponseModel> GetUserIdAsync(string userName, string token);
    }
}
