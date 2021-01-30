using LyricsCollector.Models.LyricsModels.Contracts;

namespace LyricsCollector.Services.Contracts
{
    public interface ICollectionService : IObserver
    {
        ILyricsResponseModel GetCurrentLyrics();

    }
}
