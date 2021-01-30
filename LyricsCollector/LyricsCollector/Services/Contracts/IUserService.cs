using LyricsCollector.Entities;
using LyricsCollector.Entities.Contracts;
using LyricsCollector.Models.UserModels;

namespace LyricsCollector.Services.Contracts
{
    public interface IUserService
    {
        IUser GeneratePassword(UserPostModel userPM);
        UserToken ValidatePassword(UserPostModel userPM, IUser user);
    }
}
