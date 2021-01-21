
using LyricsCollector.Entities;
using System.Collections.Generic;

namespace LyricsCollector.Models.CollectionModels
{
    public class CollectionResponseModel
    {
        public string Name { get; set; }
        public IList<CollectionLyrics> Lyrics { get; set; }
    }
}
