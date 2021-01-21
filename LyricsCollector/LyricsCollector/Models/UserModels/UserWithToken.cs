using LyricsCollector.Entities;

namespace LyricsCollector.Models.UserModels
{
    public class UserWithToken : IUserWithToken
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
