using LyricsCollector.Entities;

namespace LyricsCollector.Models
{
    public class UserWithToken
    {
        public User User { get; set; }

        public UserWithToken(User user)
        {
            User = user;
        }
    }
}
