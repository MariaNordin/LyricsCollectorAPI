
namespace LyricsCollector.Models.UserModels
{
    public class UserWithToken
    {
        public UserResponseModel User { get; set; }
        public string Token { get; set; }

        public UserWithToken(UserResponseModel user, string token)
        {
            User = user;
            Token = token;
        }
    }
}
