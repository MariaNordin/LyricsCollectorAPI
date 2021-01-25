using LyricsCollector.Entities;
using LyricsCollector.Events;
using LyricsCollector.Models.LyricsModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface ILyricsService
    {
        event EventHandler<LyricsEventArgs> LyricsFound;
        Task<LyricsResponseModel> Search(string artist, string title);

        Task<bool> SaveCollectionLyricsAsync(LyricsResponseModel lyrics, int userId, int collectionId);

        IEnumerable<Lyrics> GetDbLyrics(); // Private? 
    }
}
