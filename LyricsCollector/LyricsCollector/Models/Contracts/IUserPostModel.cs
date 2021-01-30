
namespace LyricsCollector.Models.Contracts
{
    public interface IUserPostModel
    {
        public string Password { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }
        public int CollectionId { get; set; }
        public string NewCollectionName { get; set; }
    }
}
