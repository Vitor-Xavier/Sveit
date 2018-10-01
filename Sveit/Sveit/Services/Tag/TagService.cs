using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Requests;

namespace Sveit.Services.Tag
{
    public class TagService : ITagService
    {
        private readonly IRequestService _requestService;

        public TagService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<Models.Tag> GetTag(int tagId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.TagsEndpoint);
            builder.AppendToPath(tagId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<Models.Tag>(uri);
        }

        public Task<IEnumerable<Models.Tag>> GetTagsAsync()
        {
            string uri = AppSettings.TagsEndpoint;

            return _requestService.GetAsync<IEnumerable<Models.Tag>>(uri);
        }

        public Task<IEnumerable<Models.Tag>> GetTagsByNameAsync(string name)
        {
            UriBuilder builder = new UriBuilder(AppSettings.TagsEndpoint);
            builder.AppendToPath("Name");
            builder.AppendToPath(name);
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Tag>>(uri);
        }

        public Task<bool> PostTagAsync(Models.Tag tag)
        {
            string uri = AppSettings.TagsEndpoint;

            return _requestService.PostAsync<Models.Tag, bool>(uri, tag);
        }

        public Task<bool> DeleteTagAsync(int tagId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.TagsEndpoint);
            builder.AppendToPath(tagId.ToString());
            string uri = builder.ToString();

            return _requestService.DeleteAsync(uri, "");
        }
    }
}
