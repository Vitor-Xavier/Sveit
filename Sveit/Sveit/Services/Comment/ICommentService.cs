using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.Services.Comment
{
    interface ICommentService
    {
        Task<Models.Comment> GetById(int commentId);

        Task<IEnumerable<Models.Comment>> GetCommentsToPlayer(int playerId, DateTime? initialDate = null, DateTime? finalDate = null);

        Task<IEnumerable<Models.Comment>> GetCommentsFromPlayer(int playerId, DateTime? initialDate = null, DateTime? finalDate = null);

        Task<bool> PostComment(Models.Comment comment);

        Task<bool> DeleteComment(int commentId);
    }
}
