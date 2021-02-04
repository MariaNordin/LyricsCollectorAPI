using LyricsCollector.Models.SpotifyModels.Contracts;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface ISpotifyService
    {
        public Task GetAccessTokenAsync();
        public Task<ITrackResponseModel> SearchAsync(string artist, string title);
    }
}
