using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Sveit.Extensions;
using Sveit.Services.Requests;

namespace Sveit.Services.Content
{
    class ContentService : IContentService
    {
        private readonly IRequestService _requestService;

        public ContentService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<IEnumerable<Models.Content>> GetContentsAsync(DateTime? initialDate = null, DateTime? finalDate = null)
        {
            UriBuilder builder = new UriBuilder(AppSettings.ContentsEndpoint);

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["initialDate"] = initialDate.ToString();
            query["finalDate"] = finalDate.ToString();

            builder.Query += query.ToString();
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Content>>(uri);
        }

        public Task<bool> PostContentAsync(Models.Content content)
        {
            string uri = AppSettings.ContentsEndpoint;

            return _requestService.PostAsync<Models.Content, bool>(uri, content);
        }

        public Task<bool> DeleteContentAsync(int contentId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.ContentsEndpoint);
            builder.AppendToPath(contentId.ToString());
            string uri = builder.ToString();

            return _requestService.DeleteAsync(uri, "");
        }
    }
}
