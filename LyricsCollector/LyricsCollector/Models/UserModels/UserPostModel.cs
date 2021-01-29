
using System.ComponentModel.DataAnnotations;

namespace LyricsCollector.Models.UserModels
{
    public class UserPostModel
    {
        public string Password { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }
        public int CollectionId { get; set; }
        public string NewCollectionName { get; set; }
    }
}
