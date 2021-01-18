using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyricsCollector.Services.Contracts
{
    interface ISpotifyService
    {
        public Task<SpotifyTokenModel> GetAccessToken();
        public Task<TrackResponseModel> GetThisTrack();
        public Task<UserResponseModel> GetUserIdAsync(string userName, string token);
        public Task<PlaylistsResponseModel> GetPlaylistsAsync(string token, string userId);
    }
}
}
