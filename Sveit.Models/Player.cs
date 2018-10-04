using System;
using System.Collections.Generic;

namespace Sveit.Models
{
    public class Player : EntityBase
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual Gender Gender { get; set; }
        public string Nickname { get; set; }
        public string AvatarSource { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }

        public Player()
        {
            Contacts = new HashSet<Contact>();
        }

        public override bool Equals(object obj) => obj is Player player && 
            player.PlayerId == PlayerId;

        public override int GetHashCode()
        {
            return 956575109 + PlayerId.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
