using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Observer.Observer;
using LyricsCollector.Services.Contracts;

namespace LyricsCollector.Services.ConcreteServices
{
    public class CollectionService : ICollectionService
    {
        private User _user;

        public CollectionService()
        {
        }


        public void Update(User user)
        {
            _user = user;
        }

        public void CreateDefaultCollection()
        {
            var collection = new Collection
            {
                Name = "MyLyrics",
                User = _user
            };
        }
    }
}
