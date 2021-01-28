using LyricsCollector.Entities;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface IDbUser
    {
        Task<User> GetUserAsync(string userName);
        Task<bool> SaveUserAsync(User user);
    }
}
