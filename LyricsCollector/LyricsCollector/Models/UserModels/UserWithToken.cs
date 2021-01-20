using LyricsCollector.Entities;
using LyricsCollector.Models.Contracts;
using LyricsCollector.Services.Contracts;
using System.Collections.Generic;

namespace LyricsCollector.Models.UserModels
{
    public class UserWithToken : IUserWithToken
    {
        private List<IService> _observers;

        private User _user;
        public User User 
        {
            get { return _user; } 
            set { _user = value; }
        }
        
        public string Token { get; set; }

        public UserWithToken(User user, string token)
        {
            User = user;
            Token = token;
            _observers = new List<IService>();
        }

        public void Attach(IService service)
        {
            _observers.Add(service);
        }

        public void Detach(IService service)
        {
            _observers.Remove(service);
        }

        public void NotifyService()
        {
            foreach (var observer in _observers)
            {
                observer.Notify(_user);
            }
        }
    }
}
