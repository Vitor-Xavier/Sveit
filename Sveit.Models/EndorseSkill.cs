using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sveit.Models
{
    public class EndorseSkill : EntityBase
    {
        [Key]
        [ForeignKey("PlayerSkill"), Column(Order = 0)]
        public int PlayerSkill_PlayerId { get; set; }
        [Key]
        [ForeignKey("PlayerSkill"), Column(Order = 1)]
        public int PlayerSkill_SkillId { get; set; }
        public virtual PlayerSkill PlayerSkill { get; set; }
        [Key, Column(Order = 2)]
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public override bool Equals(object obj) => obj is EndorseSkill skill &&
                   PlayerSkill_PlayerId == skill.PlayerSkill_PlayerId &&
                   PlayerSkill_SkillId == skill.PlayerSkill_SkillId &&
                   PlayerId == skill.PlayerId;

        public override int GetHashCode()
        {
            var hashCode = 437912321;
            hashCode = hashCode * -1521134295 + PlayerSkill_PlayerId.GetHashCode();
            hashCode = hashCode * -1521134295 + PlayerSkill_SkillId.GetHashCode();
            hashCode = hashCode * -1521134295 + PlayerId.GetHashCode();
            return hashCode;
        }
    }
}