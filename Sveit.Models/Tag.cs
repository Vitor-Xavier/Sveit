using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sveit.Models
{
    public class Tag : EntityBase
    {
        public int TagId { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Content> Contents { get; set; }

        public Tag()
        {
            Contents = new HashSet<Content>();
        }

        public override bool Equals(object obj) => obj is Tag tag && tag.TagId == TagId;

        public override int GetHashCode()
        {
            return 1948533646 + TagId.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}