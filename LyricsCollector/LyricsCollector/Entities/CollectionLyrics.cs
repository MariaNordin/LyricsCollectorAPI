
namespace LyricsCollector.Entities
{
    public class CollectionLyrics
    {
        public int CollectionId { get; set; }
        public Collection Collection { get; set; }

        public int LyricsId { get; set; }
        public Lyrics Lyrics { get; set; }
    }
}
