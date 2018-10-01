using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Requests;

namespace Sveit.Services.Comment
{
    class CommentService : ICommentService
    {
        private readonly IRequestService _requestService;

        public CommentService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<Models.Comment> GetById(int commentId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.CommentsEndpoint);
            builder.AppendToPath(commentId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<Models.Comment>(uri);
        }

        public Task<IEnumerable<Models.Comment>> GetCommentsFromPlayer(int playerId, DateTime? initialDate = null, DateTime? finalDate = null)
        {
            UriBuilder builder = new UriBuilder(AppSettings.CommentsEndpoint);
            builder.AppendToPath($"From/{playerId}");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["initialDate"] = initialDate.ToString();
            query["finalDate"] = finalDate.ToString();

            builder.Query += query.ToString();
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Comment>>(uri);
        }

        public Task<IEnumerable<Models.Comment>> GetCommentsToPlayer(int playerId, DateTime? initialDate = null, DateTime? finalDate = null)
        {
            UriBuilder builder = new UriBuilder(AppSettings.CommentsEndpoint);
            builder.AppendToPath($"To/{playerId}");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["initialDate"] = initialDate.ToString();
            query["finalDate"] = finalDate.ToString();

            builder.Query += query.ToString();
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Comment>>(uri);
        }

        public Task<bool> PostComment(Models.Comment comment)
        {
            UriBuilder builder = new UriBuilder(AppSettings.CommentsEndpoint);
            string uri = builder.ToString();

            return _requestService.PostAsync<Models.Comment, bool>(uri, comment);
        }

        public Task<bool> DeleteComment(int commentId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.CommentsEndpoint);
            builder.AppendToPath(commentId.ToString());
            string uri = builder.ToString();

            return _requestService.DeleteAsync(uri, "");
        }
    }
}
