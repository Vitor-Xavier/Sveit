using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sveit.Models
{
    public class TeamPlayer : EntityBase
    {
        [Key, Column(Order = 0)]
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
        [Key, Column(Order = 1)]
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public override bool Equals(object obj) => obj is TeamPlayer teamPlayer &&
                teamPlayer.PlayerId == PlayerId &&
                teamPlayer.TeamId == TeamId;

        public override int GetHashCode()
        {
            var hashCode = -1709723354;
            hashCode = hashCode * -1521134295 + TeamId.GetHashCode();
            hashCode = hashCode * -1521134295 + PlayerId.GetHashCode();
            return hashCode;
        }
    }
}