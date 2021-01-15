using LyricsCollector.Entities;

namespace LyricsCollector.Models
{
    public class AuthenticateResponseModel
    {
        public User User { get; set; }
        public string Token { get; set; }

        public AuthenticateResponseModel(User user, string token)
        {
            User = user;
            Token = token;
        }
    }
}
