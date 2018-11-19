using System.ComponentModel.DataAnnotations;

namespace Sveit.Models
{
    public class RoleType : EntityBase
    {
        public int RoleTypeId { get; set; }
        [Required, StringLength(40)]
        public string Name { get; set; }
        [StringLength(40)]
        public string IconSource { get; set; }

        public override bool Equals(object obj)
        {
            return obj is RoleType type &&
                   RoleTypeId == type.RoleTypeId;
        }

        public override int GetHashCode()
        {
            return 1698171428 + RoleTypeId.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}