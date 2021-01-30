using LyricsCollector.Entities.Contracts;
using LyricsCollector.Models.Contracts;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts.IDbHelpers
{
    public interface IDbUsers
    {
        Task<IUser> GetUserAsync(string userName);
        Task<bool> SaveUserAsync(IUser user);
    }
}
