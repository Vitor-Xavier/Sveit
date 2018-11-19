using System.ComponentModel.DataAnnotations;

namespace Sveit.Models
{
    public class Platform : EntityBase
    {
        public int PlatformId { get; set; }
        [Required, StringLength(20)]
        public string Name { get; set; }
        [StringLength(50)]
        public string IconSource { get; set; }

        public override bool Equals(object obj) => obj is Platform platform && 
            platform.PlatformId == PlatformId;

        public override int GetHashCode()
        {
            return -1554650153 + PlatformId.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
