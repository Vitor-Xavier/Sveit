namespace Sveit.Models
{
    public class RoleType : EntityBase
    {
        public int RoleTypeId { get; set; }
        public string Name { get; set; }
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

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