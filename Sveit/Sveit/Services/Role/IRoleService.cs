using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.Services.Role
{
    public interface IRoleService
    {
        Task<Models.Role> GetRole(int roleId);

        Task<IEnumerable<Models.Role>> GetRolesAsync();

        Task<IEnumerable<Models.Role>> GetRoleByTypeAsync(int roleTypeId);
    }
}
