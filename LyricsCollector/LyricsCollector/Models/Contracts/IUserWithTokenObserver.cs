using LyricsCollector.Entities;

namespace LyricsCollector.Models.Contracts
{
    public interface IUserWithTokenObserver
    {
        void Notify(User user);
    }
}
