
namespace LyricsCollector.Models.LyricsModels
{
    public class LyricsMemento
    {
        private LyricsResponseModel _lyrics;

        public LyricsMemento(LyricsResponseModel lyrics)
        {
            _lyrics = lyrics;
        }

        public LyricsResponseModel Lyrics
        {
            get { return _lyrics; }
            set { _lyrics = value; }
        }
    }
}
