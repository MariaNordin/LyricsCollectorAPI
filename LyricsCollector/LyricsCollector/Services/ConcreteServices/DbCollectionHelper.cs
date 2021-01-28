using LyricsCollector.Context;
using LyricsCollector.Services.Contracts;

namespace LyricsCollector.Services.ConcreteServices
{
    public class DbCollectionHelper : IDbCollectionHelper
    {
        private readonly LyricsCollectorDbContext _context;

        public DbCollectionHelper(LyricsCollectorDbContext context)
        {
            _context = context;
        }
    }
}
