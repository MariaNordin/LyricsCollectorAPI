using System.Collections.Generic;

namespace LyricsCollector.Entities
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CollectionOfUserId { get; set; }
        public User User { get; set; }
        public ICollection<Lyrics> Lyrics { get; set; }
    }
}
