using LyricsCollector.Entities;
using LyricsCollector.Events;
using LyricsCollector.Models.LyricsModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface ICollectionService
    {
        //void OnRegisteredUser(object source, UserEventArgs args);
        //void OnUserLoggedIn(object source, UserEventArgs args);

        //void OnLyricsFound(object source, LyricsEventArgs args);

        Task<Collection> NewCollectionAsync(string name, string email);
        
        Task<IEnumerable<Collection>> GetCollectionAsync(int collectionId);

        Task<IEnumerable<Collection>> GetAllCollectionsAsync(string userName);
        Task<CollectionLyrics> SaveLyricsAsync(int id);
    }
}
