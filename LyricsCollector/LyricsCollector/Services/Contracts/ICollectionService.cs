
using LyricsCollector.Entities;
using LyricsCollector.Events;
using LyricsCollector.Models.CollectionModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface ICollectionService
    {
        //void OnRegisteredUser(object source, UserEventArgs args);
        //void OnUserLoggedIn(object source, UserEventArgs args);
        Task<Collection> NewCollection(string name, string email);
        Task<IEnumerable<Collection>> GetCollectionAsync(int collectionId, string email);

        Task<IEnumerable<Collection>> GetAllCollectionsAsync(string userName);
    }
}
