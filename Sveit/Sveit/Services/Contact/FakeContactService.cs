using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sveit.Models;

namespace Sveit.Services.Contact
{
    public class FakeContactService : IContactService
    {
        public Task<bool> DeleteContactAsync(int contactId)
        {
            return Task.FromResult(false);
        }

        public Task<Models.Contact> GetContactByIdAsync(int contactId)
        {
            return Task.FromResult(new Models.Contact
            {
                ContactId = 1,
                Text = "user_discord",
                ContactTypeId = 1,
                ContactType = new Models.ContactType
                {
                    ContactTypeId = 1,
                    Name = "Discord"
                }
            });
        }
    }
}
