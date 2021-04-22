using LyricsCollector.Entities;
using LyricsCollector.Models.LyricsModels.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts.IDbHelpers
{
    public interface IDbCollections
    {
        Task<Collection> GetCollectionAsync(int collectionId);
        Task<List<Collection>> GetAllCollectionsAsync(string userName);
        Task NewCollectionAsync(string CollectionName, string userName);
        Task SaveLyricsAsync(int collectionId, ILyricsResponseModel lyrics);
        Task DeleteCollectionAsync(int id, string userName);
    }
}
