using System.Collections.Generic;

namespace LyricsCollector.Entities
{
    public class Lyrics
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int LyricsOfCollectionId { get; set; }
        public Collection Collection { get; set; }
    }
}
