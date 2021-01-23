using LyricsCollector.Entities;
using LyricsCollector.Events;
using LyricsCollector.Models.UserModels;
using System;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(UserPostModel userPM);
        Task<UserToken> AuthenticateAsync(UserPostModel userPM);
        Task<UserResponseModel> GetUserAsync(string name);
    }
}
