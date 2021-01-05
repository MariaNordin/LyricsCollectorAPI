using LyricsCollector.SpotifyClasses;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface ISpotifyService
    {
        public Task<SpotifyToken> GetAccessToken();
    }
}
