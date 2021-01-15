using LyricsCollector.Entities;
using LyricsCollector.Models;

namespace LyricsCollector.Services.Contracts
{
    public interface IUserService
    {
        public User RegisterUser(UserPostModel user);
        UserResponseModel Authenticate(UserPostModel user);
    }
}
