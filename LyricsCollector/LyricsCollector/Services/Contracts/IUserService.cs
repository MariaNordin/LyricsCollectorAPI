using LyricsCollector.Entities;
using LyricsCollector.Models.UserModels;

namespace LyricsCollector.Services.Contracts
{
    public interface IUserService
    {
        User GeneratePassword(UserPostModel userPM);
        UserToken ValidatePassword(UserPostModel userPM, User user);
    }
}
