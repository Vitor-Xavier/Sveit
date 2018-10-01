using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sveit.Models
{
    public class GamePlatform : EntityBase
    {
        [Key, Column(Order = 0)]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        [Key, Column(Order = 1)]
        public int PlatformId { get; set; }
        public virtual Platform Platform { get; set; }

        public override bool Equals(object obj) => obj is GamePlatform platform &&
                   GameId == platform.GameId &&
                   PlatformId == platform.PlatformId;

        public override int GetHashCode()
        {
            var hashCode = -189556903;
            hashCode = hashCode * -1521134295 + GameId.GetHashCode();
            hashCode = hashCode * -1521134295 + PlatformId.GetHashCode();
            return hashCode;
        }
    }
}