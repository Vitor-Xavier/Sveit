using System.Collections.Generic;

namespace Sveit.Models
{
    public class Content : EntityBase
    {
        public int ContentId { get; set; }
        public string Title { get; set; }
        public virtual Game Game { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string ImageSource { get; set; }
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
