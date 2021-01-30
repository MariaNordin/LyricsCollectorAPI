using LyricsCollector.Models.UserModels.Contracts;

namespace LyricsCollector.Models.UserModels
{
    public class UserToken : IUserToken
    {
        public string Token { get; set; }
    }
}
