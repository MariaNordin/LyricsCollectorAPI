using LyricsCollector.Models;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface ILyricsService
    {
        public Task<LyricsResponseModel> Search(string artist, string title);
    }
}
