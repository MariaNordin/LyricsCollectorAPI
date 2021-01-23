using LyricsCollector.Entities;
using System.Collections.Generic;

namespace LyricsCollector.Models.UserModels
{
    public class UserResponseModel
    {
        public string Name { get; set; }
        public IList<Collection> Collections { get; set; }
    }
}
