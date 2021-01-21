using LyricsCollector.Entities;
using LyricsCollector.Models.LyricsModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface ILyricsService : IService
    {

        public Task<LyricsResponseModel> Search(string artist, string title);

        public Task<bool> SaveCollectionLyricsAsync(LyricsResponseModel lyrics, int userId, int collectionId);

        public IEnumerable<Lyrics> GetDbLyrics(); // Private? 
    }
}
