using LyricsCollector.Entities;
using LyricsCollector.Observer.Observer;
using System.Collections.Generic;

namespace LyricsCollector.Models.UserModels
{
    public class UserWithToken : IUserWithToken
    {
        private List<ILoggedInUserObserver> _observers;

        public UserWithToken()
        {
            _observers = new List<ILoggedInUserObserver>();
        }

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

        public void AttachObserver(ILoggedInUserObserver observer)
        {
            _observers.Add(observer);
        }

        public void DetachObserver(ILoggedInUserObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObserver()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_user);
            }
        }
    }
}
