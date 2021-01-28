using LyricsCollector.Entities;
using LyricsCollector.Models.UserModels;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts.IDbHelpers
{
    public interface IDbUsers
    {
        Task<User> GetUserAsync(string userName);
        Task<bool> SaveUserAsync(User user);
    }
}
