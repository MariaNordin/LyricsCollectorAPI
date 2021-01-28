using LyricsCollector.Entities;
using LyricsCollector.Models.LyricsModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface IDbLyrics
    {
        Task<List<Lyrics>> GetDbLyricsAsync();
        LyricsResponseModel LyricsInDbMatch(string artist, string title);
        Task SaveLyricsToDbAsync(LyricsResponseModel lyricsRM);
    }
}
