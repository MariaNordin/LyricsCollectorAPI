using System.Collections.Generic;

namespace LyricsCollector.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; } 
        public string Hash { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public IList<Collection> Collections { get; set; }

    }
}
