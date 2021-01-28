using LyricsCollector.Entities;
using LyricsCollector.Models.UserModels;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface IDbUserService
    {
        Task<User> GetUser(UserPostModel userPM);
        Task<bool> SaveUserAsync(User user);
    }
}
