using System.Collections.Generic;

namespace Sveit.Models
{
    public class Team : EntityBase
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public int GamePlatform_GameId { get; set; }
        public int GamePlatform_PlatformId { get; set; }
        public virtual GamePlatform GamePlatform { get; set; }
        public int OwnerId { get; set; }
        public virtual Player Owner { get; set; }
        public string IconSource { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }

        public Team()
        {
            Contacts = new HashSet<Contact>();
        }

        public override bool Equals(object obj) => obj is Team team && 
            team.TeamId == TeamId;

        public override int GetHashCode()
        {
            return -1532736471 + TeamId.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
