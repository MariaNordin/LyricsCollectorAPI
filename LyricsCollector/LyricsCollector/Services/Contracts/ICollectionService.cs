using LyricsCollector.Models.LyricsModels;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    public interface ICollectionService
    {
        public Task<bool> SaveCollectionLyricsAsync(LyricsResponseModel lyrics, int userId, int collectionId);
    }
}
