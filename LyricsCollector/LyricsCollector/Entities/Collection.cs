using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LyricsCollector.Entities
{
    public class Collection
    {
        [ForeignKey ("User")]

        public int Id { get; set; }
        public string Name { get; set; }
        public int CollectionOfUserId { get; set; }
        public User User { get; set; }
        public IList<CollectionLyrics> Lyrics { get; set; }
    }
}
