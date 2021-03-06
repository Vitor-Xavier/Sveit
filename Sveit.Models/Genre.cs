﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sveit.Models
{
    public class Genre : EntityBase
    {
        public int GenreId { get; set; }
        [Required, StringLength(25)]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Game> Games { get; set; }

        public Genre()
        {
            Games = new HashSet<Game>();
        }

        public override bool Equals(object obj) => obj is Genre genre && 
            genre.GenreId == GenreId;

        public override int GetHashCode()
        {
            return 538975685 + GenreId.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
