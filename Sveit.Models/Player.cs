using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sveit.Models
{
    public class Player : EntityBase
    {
        public int PlayerId { get; set; }
        [Required, StringLength(40)]
        public string Username { get; set; }
        [Required, StringLength(50)]
        public string Email { get; set; }
        [Required, StringLength(300)]
        public string Password { get; set; }
        [Required, StringLength(60)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int GenderId { get; set; }
        public virtual Gender Gender { get; set; }
        [Required, StringLength(30)]
        public string Nickname { get; set; }
        [StringLength(300)]
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
