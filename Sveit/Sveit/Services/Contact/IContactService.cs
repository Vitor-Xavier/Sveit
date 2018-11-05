using System.Threading.Tasks;

namespace Sveit.Services.Contact
{
    public interface IContactService
    {
        Task<Models.Contact> GetContactByIdAsync(int contactId);

        Task<bool> DeleteContactAsync(int contactId);
    }
}
