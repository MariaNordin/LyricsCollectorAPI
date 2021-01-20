using LyricsCollector.Context;
using LyricsCollector.Models.UserModels;
using LyricsCollector.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyricsCollector.Services.ConcreteServices
{
    public class CollectionService : ICollectionService
    {
        private readonly LyricsCollectorDbContext _context;
        public CollectionService(LyricsCollectorDbContext context)
        {
            _context = context;
        }

        public void Notify(UserWithToken user)
        {
        }
    }
}
