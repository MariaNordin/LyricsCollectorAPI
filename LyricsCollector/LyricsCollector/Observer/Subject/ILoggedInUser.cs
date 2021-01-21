using LyricsCollector.Models.UserModels;
using LyricsCollector.Observer.Observer;

namespace LyricsCollector.Observer.Subject
{
    public interface ILoggedInUser
    {
        void AttachObserver(ILoggedInUserObserver observer);
        void DetachObserver(ILoggedInUserObserver observer);
        void NotifyObserver(UserWithToken userWithToken);
    }
}
