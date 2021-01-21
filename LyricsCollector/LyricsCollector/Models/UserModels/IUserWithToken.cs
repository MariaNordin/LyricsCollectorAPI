using LyricsCollector.Models.UserModels;
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
