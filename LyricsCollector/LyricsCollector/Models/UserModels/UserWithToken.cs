using LyricsCollector.Entities;
using LyricsCollector.Models.Contracts;
using LyricsCollector.Services.Contracts;
using System.Collections.Generic;

namespace LyricsCollector.Models.UserModels
{
    public class UserWithToken : IUserWithToken
    {
        private readonly List<IUserWithTokenObserver> _observers;

        private User _user;
        public User User 
        {
            get { return _user; } 
            set 
            { 
                _user = value;
                NotifyObserver();
            }
        }
        
        public string Token { get; set; }

        public UserWithToken()
        {
            _observers = new List<IUserWithTokenObserver>();
        }

        public void AttachObserver(IUserWithTokenObserver observer)
        {
            _observers.Add(observer);
        }

        public void DetachObserver(IUserWithTokenObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObserver()
        {
            foreach (var observer in _observers)
            {
                observer.Notify(_user);
            }
        }
    }
}
