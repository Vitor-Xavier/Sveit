using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sveit.Models
{
    public class Apply : EntityBase
    {
        public int ApplyId { get; set; }
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
        public int VacancyId { get; set; }
        public virtual Vacancy Vacancy { get; set; }
        [StringLength(300)]
        public string Text { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public bool? Approved { get; set; }

        public Apply()
        {
            Roles = new HashSet<Role>();
        }

        public override bool Equals(object obj) => obj is Apply apply && 
            apply.ApplyId == ApplyId;

        public override int GetHashCode()
        {
            return -994520922 + ApplyId.GetHashCode();
        }

    }
}
