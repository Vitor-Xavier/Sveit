using System.ComponentModel.DataAnnotations;

namespace Sveit.Models
{
    public class Contact : EntityBase
    {
        public int ContactId { get; set; }
        [StringLength(50)]
        public string Text { get; set; }
        public int ContactTypeId { get; set; }
        public virtual ContactType ContactType { get; set; }

        public override bool Equals(object obj) => obj is Contact contact && 
            contact.ContactId == ContactId;

        public override int GetHashCode()
        {
            return 113581534 + ContactId.GetHashCode();
        }
    }
}
