using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyricsCollector.Models.Contracts
{
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Notify();
    }
}
