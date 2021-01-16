using LyricsCollector.Entities;
using LyricsCollector.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface IUserService
    {
        //public User RegisterUser(UserPostModel user);
        public Task<ActionResult<User>> RegisterUser(UserPostModel userPM);
        public Task<ActionResult<UserWithToken>> Authenticate(UserPostModel userPM);
        //UserResponseModel Authenticate(UserPostModel user);
    }
}
