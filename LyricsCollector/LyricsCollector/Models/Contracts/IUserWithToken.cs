using LyricsCollector.Services.Contracts;

namespace LyricsCollector.Models.Contracts
{
    public interface IUserWithToken
    {
        void AttachObserver(IUserWithTokenObserver observer);
        void DetachObserver(IUserWithTokenObserver observer);
        void NotifyObserver();
    }
}
