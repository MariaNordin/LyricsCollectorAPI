using LyricsCollector.Entities;
using LyricsCollector.Models.LyricsModels.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts.IDbHelpers
{
    public interface IDbCollections
    {
        Task<Collection> GetCollectionWithLyricsAsync(int collectionId);
        Task<List<Collection>> GetUsersAllCollectionsAsync(string userName);
        Task NewCollectionAsync(string CollectionName, string userName);
        Task SaveLyricsAsync(int collectionId, ILyricsResponseModel lyrics);
        Task DeleteCollectionAsync(int id);
    }
}
