﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Login;
using Sveit.Services.Requests;

namespace Sveit.Services.Comment
{
    public class CommentService : ICommentService
    {
        private readonly IRequestService _requestService;

        private readonly ILoginService _loginService;

        public CommentService(IRequestService requestService)
        {
            _requestService = requestService;
            _loginService = new LoginService(_requestService);
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

        public async Task<Models.Comment> PostComment(Models.Comment comment)
        {
            UriBuilder builder = new UriBuilder(AppSettings.CommentsEndpoint);
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.PostAsync<Models.Comment, Models.Comment>(uri, comment, token);
        }

        public async Task<bool> DeleteComment(int commentId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.CommentsEndpoint);
            builder.AppendToPath(commentId.ToString());
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.DeleteAsync(uri, token);
        }
    }
}
