using LyricsCollector.Entities;
using LyricsCollector.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface IUserService
    {
        public Task<ActionResult<User>> RegisterUser(UserPostModel userPM);
        public Task<UserWithToken> Authenticate(UserPostModel userPM);
    }
}
