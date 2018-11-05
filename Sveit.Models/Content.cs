using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sveit.Models
{
    public class Content : EntityBase
    {
        public int ContentId { get; set; }
        [StringLength(30)]
        public string Title { get; set; }
        public virtual Game Game { get; set; }
        [StringLength(300)]
        public string Description { get; set; }
        [StringLength(50)]
        public string Source { get; set; }
        [StringLength(300)]
        public string ImageSource { get; set; }
        [StringLength(255)]
        public string ContentUrl { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

        public Content()
        {
            Tags = new HashSet<Tag>();
        }

        public override bool Equals(object obj) => obj is Content content &&
            content.ContentId == ContentId;

        public override int GetHashCode()
        {
            return -229040473 + ContentId.GetHashCode();
        }
    }
}
