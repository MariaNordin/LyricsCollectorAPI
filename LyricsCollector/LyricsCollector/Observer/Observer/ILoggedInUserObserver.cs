using LyricsCollector.Entities;

namespace LyricsCollector.Observer.Observer
{
    public interface ILoggedInUserObserver
    {
        void Update(User user);
    }
}
