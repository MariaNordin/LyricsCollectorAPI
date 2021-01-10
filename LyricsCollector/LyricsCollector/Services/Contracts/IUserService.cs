using LyricsCollector.Entities;
using LyricsCollector.Models;

namespace LyricsCollector.Services.Contracts
{
    public interface IUserService
    {
        User RegisterUser(UserPostModel user);
        AuthenticateResponse Authenticate(UserPostModel user);
    }
}
