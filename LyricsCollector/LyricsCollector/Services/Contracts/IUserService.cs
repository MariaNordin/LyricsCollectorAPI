using LyricsCollector.Entities.Contracts;
using LyricsCollector.Models.UserModels.Contracts;

namespace LyricsCollector.Services.Contracts
{
    public interface IUserService
    {
        IUser GeneratePassword(IUserPostModel userPM);
        IUserToken ValidatePassword(IUserPostModel userPM, IUser user);
    }
}
