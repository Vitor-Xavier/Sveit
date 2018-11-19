using System.ComponentModel.DataAnnotations;

namespace Sveit.Models
{
    public class Comment : EntityBase
    {
        public int CommentId { get; set; }
        [Required, StringLength(300)]
        public string Text { get; set; }
        public int FromPlayerId { get; set; }
        public virtual Player FromPlayer { get; set; }
        public int ToPlayerId { get; set; }
        public virtual Player ToPlayer { get; set; }

        public override bool Equals(object obj) => obj is Comment comment && 
            comment.CommentId == CommentId;

        public override int GetHashCode()
        {
            return -1762522277 + CommentId.GetHashCode();
        }
    }
}
