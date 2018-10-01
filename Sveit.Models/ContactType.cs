namespace Sveit.Models
{
    public class ContactType : EntityBase
    {
        public int ContactTypeId { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj) => obj is ContactType contactType && 
            contactType.ContactTypeId == ContactTypeId;

        public override int GetHashCode()
        {
            return 1836415718 + ContactTypeId.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}