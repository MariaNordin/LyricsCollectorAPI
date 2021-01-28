using LyricsCollector.Models.LyricsModels;

namespace LyricsCollector.Services.Contracts
{
    public interface IObserver
    {
        void Update(LyricsResponseModel lyrics);
    }
}
