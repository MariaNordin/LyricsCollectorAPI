using LyricsCollector.Models.UserModels.Contracts;

namespace LyricsCollector.Models.UserModels
{
    public class UserPostModel : IUserPostModel
    {
        public string Password { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }
        public int CollectionId { get; set; }
        public string NewCollectionName { get; set; }
    }
}
