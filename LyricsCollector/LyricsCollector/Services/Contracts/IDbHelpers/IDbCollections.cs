using LyricsCollector.Entities;
using LyricsCollector.Models.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts.IDbHelpers
{
    public interface IDbCollections
    {
        Task<Collection> GetCollectionAsync(int collectionId);
        Task<IEnumerable<Collection>> GetAllCollectionsAsync(string userName);
        Task NewCollectionAsync(string CollectionName, string userName);
        Task<bool> SaveLyricsAsync(int collectionId, ILyricsResponseModel lyrics);
    }
}
