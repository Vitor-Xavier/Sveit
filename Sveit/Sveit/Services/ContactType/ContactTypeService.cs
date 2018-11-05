using Sveit.Extensions;
using Sveit.Services.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sveit.Services.ContactType
{
    public class ContactTypeService : IContactTypeService
    {
        private readonly IRequestService _requestService;

        public ContactTypeService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<IEnumerable<Models.ContactType>> GetContactTypesAsync()
        {
            string uri = AppSettings.ContactTypesEndpoint;

            return _requestService.GetAsync<IEnumerable<Models.ContactType>>(uri);
        }

        public Task<Models.ContactType> GetContactTypeAsync(int contactTypeId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.ContactTypesEndpoint);
            builder.AppendToPath(contactTypeId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<Models.ContactType>(uri);
        }

        public Task<Models.ContactType> PostContactTypeAsync(Models.ContactType contactType)
        {
            string uri = AppSettings.ContactTypesEndpoint;

            return _requestService.PostAsync<Models.ContactType, Models.ContactType>(uri, contactType);
        }

        public Task<bool> DeleteContactTypeAsync(int contactTypeId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.ContactTypesEndpoint);
            builder.AppendToPath(contactTypeId.ToString());
            string uri = builder.ToString();

            return _requestService.DeleteAsync(uri, "");
        }
    }
}
