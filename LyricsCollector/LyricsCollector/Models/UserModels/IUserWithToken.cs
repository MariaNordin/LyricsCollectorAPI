using LyricsCollector.Entities;
using LyricsCollector.Observer.Observer;

namespace LyricsCollector.Models.UserModels
{
    public interface IUserWithToken
    {
        void AttachObserver(ILoggedInUserObserver observer);
        void DetachObserver(ILoggedInUserObserver observer);
        void NotifyObserver();
    }
}
