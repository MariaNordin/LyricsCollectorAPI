using LyricsCollector.Entities;
using LyricsCollector.Models;
using Microsoft.Extensions.DependencyInjection;

namespace LyricsCollector.Services.Contracts
{
    public interface IUserService
    {
        User RegisterUser(UserPostModel user);
        AuthenticateResponse Authenticate(UserPostModel user);
    }
}
