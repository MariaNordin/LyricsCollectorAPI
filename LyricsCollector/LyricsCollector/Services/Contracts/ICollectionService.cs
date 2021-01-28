using LyricsCollector.Models.LyricsModels;

namespace LyricsCollector.Services.Contracts
{
    public interface ICollectionService : IObserver
    {
        LyricsResponseModel GetCurrentLyrics();

        //void OnRegisteredUser(object source, UserEventArgs args);
        //void OnUserLoggedIn(object source, UserEventArgs args);

        //void OnLyricsFound(object source, LyricsEventArgs args);

    }
}
