using LyricsCollector.Entities;
using LyricsCollector.Events;
using LyricsCollector.Models.UserModels;
using System;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface IUserService
    {
        public Task<User> RegisterUser(UserPostModel userPM);
        public Task<UserWithToken> Authenticate(UserPostModel userPM);
        
        public event EventHandler<UserEventArgs> UserLoggedIn;

        public event EventHandler<UserEventArgs> RegisteredUser;
    }
}
