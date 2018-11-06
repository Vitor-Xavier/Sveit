using Sveit.Extensions;
using Sveit.Services.Login;
using Sveit.Services.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sveit.Services.ContactType
{
    public class ContactTypeService : IContactTypeService
    {
        private readonly IRequestService _requestService;

        private readonly ILoginService _loginService;

        public ContactTypeService(IRequestService requestService)
        {
            _requestService = requestService;
            _loginService = new LoginService(_requestService);
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

        public async Task<Models.ContactType> PostContactTypeAsync(Models.ContactType contactType)
        {
            string uri = AppSettings.ContactTypesEndpoint;

            var token = await _loginService.GetOAuthToken();
            return await _requestService.PostAsync<Models.ContactType, Models.ContactType>(uri, contactType, token);
        }

        public async Task<bool> DeleteContactTypeAsync(int contactTypeId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.ContactTypesEndpoint);
            builder.AppendToPath(contactTypeId.ToString());
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.DeleteAsync(uri, token);
        }
    }
}
