﻿using LyricsCollector.Entities.Contracts;
using System.Collections.Generic;

namespace LyricsCollector.Entities
{
    public class User : IUser
    {
        public int Id { get; set; }
        public byte[] Salt { get; set; } 
        public string Hash { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public IList<Collection> Collections { get; set; }

    }
}
