using LyricsCollector.Entities;
using LyricsCollector.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface ILyricsService
    {
        public Task<LyricsResponseModel> Search(string artist, string title);
        public IEnumerable<Lyrics> GetDbLyrics(); // Private? 
    }
}
