using LyricsCollector.Services.Contracts;

namespace LyricsCollector.Models.Contracts
{
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Notify();
    }
}
