using LyricsCollector.Entities;
using System;

namespace LyricsCollector.Events
{
    public class UserEventArgs : EventArgs
    {
        public User User { get; set; }
    }
}
