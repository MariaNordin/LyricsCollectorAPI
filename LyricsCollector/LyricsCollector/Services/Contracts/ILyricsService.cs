using LyricsCollector.Models.Contracts;
using System;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface ILyricsService
    {
        Task<ILyricsResponseModel> Search(string artist, string title);
        string ToTitleCase(string text);

        void Attach(IObserver observer);

        void Notify(ILyricsResponseModel lyrics);

    }
}
