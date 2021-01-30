using LyricsCollector.Models.SpotifyModels;
using LyricsCollector.Models.SpotifyModels.Contracts;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface ISpotifyService
    {
        public Task<ISpotifyTokenModel> GetAccessTokenAsync();
        public Task<TrackResponseModel> SearchAsync(string artist, string title);
    }
}
