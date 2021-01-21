using LyricsCollector.Entities;
using LyricsCollector.Models.Contracts;
using LyricsCollector.Services.Contracts;
using System.Collections.Generic;

namespace LyricsCollector.Models.UserModels
{
    public class UserWithToken
    {
        private User _user;
        private string _token;

        public User User
        {
            get { return _user;  }
            set { _user = value; }
        }

        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }
    }
}
