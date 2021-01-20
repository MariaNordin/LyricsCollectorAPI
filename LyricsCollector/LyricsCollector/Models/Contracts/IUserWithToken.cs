using LyricsCollector.Services.Contracts;

namespace LyricsCollector.Models.Contracts
{
    public interface IUserWithToken
    {
        void Attach(IService service);
        void Detach(IService service);
        void NotifyService();
    }
}
