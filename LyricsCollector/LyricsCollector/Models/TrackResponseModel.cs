using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyricsCollector.Models
{
    public class TrackResponseModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public string Url { get; set; }
    }
}
