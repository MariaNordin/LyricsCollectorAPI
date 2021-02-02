using LyricsCollector.Models.LyricsModels.Contracts;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts.IDbHelpers
{
    public interface IDbLyrics
    {
        Task SetDbLyricsAsync();
        Task<ILyricsResponseModel> LyricsInDbMatch(string artist, string title);
        Task SaveLyricsToDbAsync(ILyricsResponseModel lyricsRM);
    }
}
