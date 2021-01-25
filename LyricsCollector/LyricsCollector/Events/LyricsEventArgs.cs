using LyricsCollector.Models.LyricsModels;
using System;

namespace LyricsCollector.Events
{
    public class LyricsEventArgs : EventArgs
    {
        public LyricsResponseModel Lyrics { get; set; }
    }
}
