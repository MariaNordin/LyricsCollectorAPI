using LyricsCollector.Models.UserModels;

namespace LyricsCollector.Observer.Observer
{
    public interface ILoggedInUserObserver
    {
        void Notify(UserWithToken userWithtoken);
    }
}
