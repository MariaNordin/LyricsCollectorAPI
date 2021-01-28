using LyricsCollector.Models.LyricsModels;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface IDbHelperService
    {
        LyricsResponseModel LyricsInDbMatch(string artist, string title);
        Task SaveLyricsToDb(LyricsResponseModel lyricsRM);
    }
}
