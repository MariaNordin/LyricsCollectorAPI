using System.Collections.Generic;

namespace LyricsCollector.Entities
{
    public class Lyrics
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string SongLyrics { get; set; }
        public string SpotifyLink { get; set; }
        public string CoverImage { get; set; }
        public IList<CollectionLyrics> CollectionLyrics { get; set; }
    }
}
