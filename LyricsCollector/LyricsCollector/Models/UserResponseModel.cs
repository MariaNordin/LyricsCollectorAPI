using LyricsCollector.Entities;
using System.Collections.Generic;

namespace LyricsCollector.Models
{
    public class UserResponseModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int CollectionId { get; set; } // behöver jag denna? om jag från frontend vill skicka en specifik lista som user vill lägga till en låt i
        public IList<Collection> Collections { get; set; }
    }
}
