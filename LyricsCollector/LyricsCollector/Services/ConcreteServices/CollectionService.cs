using LyricsCollector.Models.LyricsModels;
using LyricsCollector.Services.Contracts;

namespace LyricsCollector.Services.ConcreteServices
{
    public class CollectionService : ICollectionService
    {
        private LyricsResponseModel _lyrics;
        
        public LyricsResponseModel GetCurrentLyrics()
        {
            return _lyrics;
        }

        public void Update(LyricsResponseModel lyrics)
        {
            _lyrics = lyrics;
        }
    }
}
