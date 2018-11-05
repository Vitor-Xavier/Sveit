using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sveit.Services.ContactType
{
    public interface IContactTypeService
    {
        Task<Models.ContactType> GetContactTypeAsync(int contactTypeId);

        Task<IEnumerable<Models.ContactType>> GetContactTypesAsync();

        Task<Models.ContactType> PostContactTypeAsync(Models.ContactType contactType);

        Task<bool> DeleteContactTypeAsync(int contactTypeId);
    }
}
