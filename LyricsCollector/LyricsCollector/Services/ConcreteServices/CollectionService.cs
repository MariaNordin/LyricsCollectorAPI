using LyricsCollector.Context;
using LyricsCollector.Entities;
using LyricsCollector.Models.LyricsModels;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Observer.Observer;
using LyricsCollector.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        //public async Task<bool> SaveCollectionLyricsAsync(LyricsResponseModel lyrics, int userId, int collectionId)
        //{

        //    var collectionLyrics = new CollectionLyrics();

        //    _context.CollectionLyrics.Add(collectionLyrics);

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        //{
        //    var existingCollection = _context.Collections.Where(c => c.CollectionOfUserId == userId).FirstOrDefault();

        //    if (existingCollectionLyrics != null)
        //    {

        //    }
        //    var existingCollectionLyrics = _context.CollectionLyrics.Where(cl => cl.CollectionId == userRM.CollectionId).FirstOrDefault();

        //    //var existingLyrics = _context.Lyrics.Where(l => l.)
        //    //var isInList = CheckLyricsInExistingList(lyricsRM, userRM.CollectionId);

        //    var lyrics = new Lyrics
        //    {
        //        Artist = lyricsRM.Artist,
        //        Title = lyricsRM.Title,
        //        SongLyrics = lyricsRM.Lyrics
        //    };

        //    if (existingCollection != null)
        //    {
        //        existingCollection.Lyrics.Add(lyrics);
        //    }
        //    return "hej";

        //}

        //private bool CheckLyricsInExistingList(LyricsResponseModel lyricsRM, int collectionId)
        //{
        //    var existingLyrics = _context.Collections.Where(c => c.Id == collectionId)
        //}
    }
}
