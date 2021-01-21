using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Observer.Observer;
using LyricsCollector.Services.Contracts;

namespace LyricsCollector.Services.ConcreteServices
{
    public class CollectionService : ICollectionService, ILoggedInUserObserver
    {
        private readonly LyricsCollectorDbContext _context;
        private UserWithToken _userWithToken;
        //private User _user;

        public CollectionService(LyricsCollectorDbContext context)
        {
            _context = context;
            _userWithToken = new UserWithToken();
            _userWithToken.AttachObserver(this);
        }


        public void Update(User user)
        {
            _userWithToken.User = user;
        }

        public void CreateDefaultCollection()
        {
            var collection = new Collection
            {
                Name = "MyLyrics",
                User = _userWithToken.User
            };

            _context.Collections.Add(collection);

            _context.SaveChanges();
        }
    }
}
