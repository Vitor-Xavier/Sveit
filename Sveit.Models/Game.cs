using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sveit.Models
{
    public class Game : EntityBase
    {
        public int GameId { get; set; }
        [Required, StringLength(40)]
        public string Name { get; set; }
        [StringLength(150)]
        public string IconSource { get; set; }
        [StringLength(150)]
        public string ImageSource { get; set; }
        [StringLength(150)]
        public string BackgroundSource { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }

        public Game()
        {
            Genres = new HashSet<Genre>();
        }

        public override bool Equals(object obj) => obj is Game game && 
            game.GameId == GameId;

        public override int GetHashCode()
        {
            return 354631258 + GameId.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
