using LyricsCollector.Models.UserModels;
using System;

namespace LyricsCollector.Events
{
    public class UserEventArgs : EventArgs
    {
        public UserWithToken UserWithToken { get; set; }
    }
}
