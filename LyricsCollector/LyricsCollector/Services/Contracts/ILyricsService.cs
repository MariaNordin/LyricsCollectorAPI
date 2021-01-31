using LyricsCollector.Models.LyricsModels.Contracts;
using System;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface ILyricsService
    {
        Task<ILyricsResponseModel> SearchAsync(string artist, string title);
        string ToTitleCase(string text);

        void Attach(IObserver observer);

        void Notify(ILyricsResponseModel lyrics);

    }
}
