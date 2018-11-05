using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sveit.Models
{
    public class Vacancy : EntityBase
    {
        public int VacancyId { get; set; }
        [StringLength(60)]
        public string Title { get; set; }
        [StringLength(300)]
        public string Description { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public bool Available { get; set; }
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<Gender> Genders { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }

        public Vacancy()
        {
            Genders = new HashSet<Gender>();
            Roles = new HashSet<Role>();
            Skills = new HashSet<Skill>();
        }

        public override bool Equals(object obj) => obj is Vacancy vacancy && 
            vacancy.VacancyId == VacancyId;

        public override int GetHashCode()
        {
            return 1269417475 + VacancyId.GetHashCode();
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
