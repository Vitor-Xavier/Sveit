using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sveit.Models;

namespace Sveit.Services.ContactType
{
    public class FakeContactTypeService : IContactTypeService
    {
        public IEnumerable<Models.ContactType> ContactTypes { get; }

        public FakeContactTypeService()
        {
            ContactTypes = GetFakeContactTypes();
        }

        private IEnumerable<Models.ContactType> GetFakeContactTypes()
        {
            yield return new Models.ContactType { ContactTypeId = 1, Name = "Email", IconSource = "ic_email.png" };
            yield return new Models.ContactType { ContactTypeId = 2, Name = "Discord", IconSource = "ic_discord.png" };
            yield return new Models.ContactType { ContactTypeId = 3, Name = "Skype", IconSource = "ic_skype.png" };
            yield return new Models.ContactType { ContactTypeId = 4, Name = "TeamSpeak", IconSource = "ic_teamspeak.png" };
        }

        public Task<IEnumerable<Models.ContactType>> GetContactTypesAsync()
        {
            return Task.FromResult(ContactTypes);
        }

        public Task<Models.ContactType> GetContactTypeAsync(int contactTypeId)
        {
            return Task.FromResult(ContactTypes.First());
        }

        public Task<Models.ContactType> PostContactTypeAsync(Models.ContactType contactType)
        {
            return Task.FromResult(contactType);
        }

        public Task<bool> DeleteContactTypeAsync(int contactTypeId)
        {
            return Task.FromResult(false);
        }
    }
}
