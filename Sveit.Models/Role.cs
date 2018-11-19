using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sveit.Models
{
    public class Role : EntityBase
    {
        public int RoleId { get; set; }
        [Required, StringLength(40)]
        public string Name { get; set; }
        public int RoleTypeId { get; set; }
        public virtual RoleType RoleType { get; set; }
        [JsonIgnore]
        public virtual ICollection<Vacancy> Vacancies { get; set; }
        [JsonIgnore]
        public virtual ICollection<Apply> Applies { get; set; }

        public Role()
        {
            Vacancies = new HashSet<Vacancy>();
            Applies = new HashSet<Apply>();
        }

        public override bool Equals(object obj)
        {
            return obj is Role role &&
                   RoleId == role.RoleId;
        }

        public override int GetHashCode()
        {
            return 1359648316 + RoleId.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}