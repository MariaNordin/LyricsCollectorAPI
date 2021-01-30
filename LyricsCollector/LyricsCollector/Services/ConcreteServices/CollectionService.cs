using LyricsCollector.Models.LyricsModels.Contracts;
using LyricsCollector.Services.Contracts;

namespace LyricsCollector.Services.ConcreteServices
{
    public class CollectionService : ICollectionService
    {
        private ILyricsResponseModel _lyrics;
    
        public ILyricsResponseModel GetCurrentLyrics()
        {
            return _lyrics;
        }

        public void Update(ILyricsResponseModel lyrics)
        {
            _lyrics = lyrics;
        }
    }
}
