using LyricsCollector.Models.SpotifyModels;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface ISpotifyService
    {
        public Task<SpotifyTokenModel> GetAccessToken();
        public Task<TrackResponseModel> Search(string artist, string title);
    }
}
