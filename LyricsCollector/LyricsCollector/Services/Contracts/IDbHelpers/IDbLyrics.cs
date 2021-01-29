using LyricsCollector.Entities;
using LyricsCollector.Models.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts.IDbHelpers
{
    public interface IDbLyrics
    {
        Task<List<Lyrics>> GetDbLyricsAsync();
        ILyricsResponseModel LyricsInDbMatch(string artist, string title);
        Task SaveLyricsToDbAsync(ILyricsResponseModel lyricsRM);
    }
}
