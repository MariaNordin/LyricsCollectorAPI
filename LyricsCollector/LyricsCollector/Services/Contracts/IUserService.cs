using LyricsCollector.Entities;
using LyricsCollector.Models.UserModels;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface IUserService
    {
        public Task<User> RegisterUser(UserPostModel userPM);
        public Task<UserWithToken> Authenticate(UserPostModel userPM);
    }
}
