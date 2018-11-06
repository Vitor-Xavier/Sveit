using Sveit.Extensions;
using Sveit.Services.Login;
using Sveit.Services.Requests;
using System;
using System.Threading.Tasks;

namespace Sveit.Services.Contact
{
    public class ContactService : IContactService
    {
        private readonly IRequestService _requestService;

        private readonly ILoginService _loginService;

        public ContactService(IRequestService requestService)
        {
            _requestService = requestService;
            _loginService = new LoginService(_requestService);
        }

        public Task<Models.Contact> GetContactByIdAsync(int contactId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.ContactsEndpoint);
            builder.AppendToPath(contactId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<Models.Contact>(uri);
        }

        public async Task<bool> DeleteContactAsync(int contactId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.ContactsEndpoint);
            builder.AppendToPath(contactId.ToString());
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.DeleteAsync(uri, token);
        }
    }
}
