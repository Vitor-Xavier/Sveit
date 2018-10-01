using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sveit.Models
{
    public class PlayerSkill : EntityBase
    {
        [Key]
        [Column(Order = 0)]
        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public virtual Player Player { get; set; }
        [Key]
        [Column(Order = 1)]
        public int SkillId { get; set; }
        public virtual Skill Skill { get; set; }

        public override bool Equals(object obj) => obj is PlayerSkill skill &&
                   PlayerId == skill.PlayerId &&
                   SkillId == skill.SkillId;

        public override int GetHashCode()
        {
            var hashCode = -933518064;
            hashCode = hashCode * -1521134295 + PlayerId.GetHashCode();
            hashCode = hashCode * -1521134295 + SkillId.GetHashCode();
            return hashCode;
        }
    }
}