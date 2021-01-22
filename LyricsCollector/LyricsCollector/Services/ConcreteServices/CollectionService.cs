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
        private IUserWithToken _userWithToken;
        private User _user;

        public CollectionService(LyricsCollectorDbContext context, IUserWithToken userWithToken)
        {
            _context = context;
            _userWithToken = userWithToken;
            _userWithToken.AttachObserver(this);
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

            _context.Collections.Add(collection);

            _context.SaveChanges();
        }
    }
}
