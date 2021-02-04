
using System;

namespace LyricsCollector.Models.SpotifyModels.Contracts
{
    public interface ISpotifyTokenModel
    {
        public string Access_token { get; set; }

        public string Token_type { get; set; }

        public int Expires_in { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsExpired { get; }
    }
}
