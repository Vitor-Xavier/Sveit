using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sveit.Models
{
    public class Gender : EntityBase
    {
        public int GenderId { get; set; }
        [StringLength(15)]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Vacancy> Vacancies { get; set; }

        public Gender()
        {
            Vacancies = new HashSet<Vacancy>();
        }

        public override bool Equals(object obj) => obj is Gender gender && 
            gender.GenderId == GenderId;

        public override int GetHashCode()
        {
            return 1819301125 + GenderId.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}