using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sveit.Models
{
    public class Skill : EntityBase
    {
        public int SkillId { get; set; }
        [Required, StringLength(30)]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Vacancy> Vacancies { get; set; }

        public Skill()
        {
            Vacancies = new HashSet<Vacancy>();
        }

        public override bool Equals(object obj) => obj is Skill skill && 
            skill.SkillId == SkillId;

        public override int GetHashCode()
        {
            return 1205488707 + SkillId.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
