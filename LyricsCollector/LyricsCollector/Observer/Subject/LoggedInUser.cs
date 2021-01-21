using LyricsCollector.Observer.Observer;
using LyricsCollector.Observer.Subject;
using System.Collections.Generic;

namespace LyricsCollector.Models.UserModels
{
    public class LoggedInUser : ILoggedInUser
    {
        private List<ILoggedInUserObserver> _observers = new List<ILoggedInUserObserver>();

        public LoggedInUser()
        {
            //foreach (var observer in loggedInUserObservers)
            //{
            //    AttachObserver(observer);
            //} 
        }

        public void AttachObserver(ILoggedInUserObserver observer)
        {
            _observers.Add(observer);
        }

        public void DetachObserver(ILoggedInUserObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObserver(UserWithToken userWithToken)
        {
            foreach (var observer in _observers)
            {
                observer.Notify(userWithToken);
            }
        }
    }
}
