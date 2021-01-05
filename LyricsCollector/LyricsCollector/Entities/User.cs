
namespace LyricsCollector.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Salt { get; set; } 
        public string Hash { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

    }
}
