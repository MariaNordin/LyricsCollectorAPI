using LyricsCollector.Observer.Observer;
using LyricsCollector.Observer.Subject;
using System.Collections.Generic;

namespace LyricsCollector.Models.UserModels
{
    public class LoggedInUser : IUserWithToken
    {
        private List<ILoggedInUserObserver> _observers;

        private UserWithToken _userWithToken;

        public UserWithToken userWithToken
        {
            get { return _userWithToken; }
            set
            {
                _userWithToken = value;
                NotifyObserver();
            }
        }

        public LoggedInUser()
        {
           _observers = new List<ILoggedInUserObserver>();
        }

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
                observer.Update(_userWithToken.User);
            }
        }
    }
}
