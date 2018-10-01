using System.Collections.Generic;

namespace Sveit.Models
{
    public class Game : EntityBase
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconSource { get; set; }
        public string ImageSource { get; set; }
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
