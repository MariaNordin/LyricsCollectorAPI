
namespace LyricsCollector.Models.LyricsModels.Contracts
{
    public interface ILyricsResponseModel
    {
        public string Lyrics { get; set; }

        public string Artist { get; set; }

        public string Title { get; set; }

        public string SpotifyLink { get; set; }

        public string CoverImage { get; set; }
    }
}
