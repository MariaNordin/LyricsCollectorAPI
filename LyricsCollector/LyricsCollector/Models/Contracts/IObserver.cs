using LyricsCollector.Models.Contracts;

namespace LyricsCollector.Models.Contracts
{
    public interface IObserver
    {
        void Update(ISubject subject);
    }
}
