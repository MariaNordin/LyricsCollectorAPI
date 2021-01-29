using LyricsCollector.Models.Contracts;

namespace LyricsCollector.Services.Contracts
{
    public interface ICollectionService : IObserver
    {
        ILyricsResponseModel GetCurrentLyrics();

    }
}
