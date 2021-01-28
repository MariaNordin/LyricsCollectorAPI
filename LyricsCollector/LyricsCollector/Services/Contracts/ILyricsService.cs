using LyricsCollector.Events;
using LyricsCollector.Models.LyricsModels;
using System;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface ILyricsService
    {
        event EventHandler<LyricsEventArgs> LyricsFound;
        Task<LyricsResponseModel> Search(string artist, string title);
        string ToTitleCase(string text);

        void Attach(IObserver observer);

        void Notify(LyricsResponseModel lyrics);

    }
}
