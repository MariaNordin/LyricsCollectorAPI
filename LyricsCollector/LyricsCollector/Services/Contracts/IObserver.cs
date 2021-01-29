using LyricsCollector.Models.Contracts;

namespace LyricsCollector.Services.Contracts
{
    public interface IObserver
    {
        void Update(ILyricsResponseModel lyrics);
    }
}
