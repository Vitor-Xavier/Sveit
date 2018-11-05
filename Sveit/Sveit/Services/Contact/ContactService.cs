using Sveit.Extensions;
using Sveit.Services.Requests;
using System;
using System.Threading.Tasks;

namespace Sveit.Services.Contact
{
    public class ContactService : IContactService
    {
        private readonly IRequestService _requestService;

        public ContactService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<Models.Contact> GetContactByIdAsync(int contactId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.ContactsEndpoint);
            builder.AppendToPath(contactId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<Models.Contact>(uri);
        }

        public Task<bool> DeleteContactAsync(int contactId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.ContactsEndpoint);
            builder.AppendToPath(contactId.ToString());
            string uri = builder.ToString();

            return _requestService.DeleteAsync(uri, "");
        }
    }
}
