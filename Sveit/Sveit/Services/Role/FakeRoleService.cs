using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sveit.Models;

namespace Sveit.Services.Role
{
    public class FakeRoleService : IRoleService
    {
        public IEnumerable<Models.Role> Roles { get; }

        public FakeRoleService()
        {
            Roles = GetFakeRoles();
        }

        private IEnumerable<Models.Role> GetFakeRoles()
        {
            yield return new Models.Role
            {
                Name = "Agressivo",
                RoleType = new Models.RoleType
                {
                    RoleTypeId = 1,
                    Name = "Papel primário",
                    IconSource = "ic_primaryRole.png"
                }
            };
            yield return new Models.Role
            {
                Name = "DPS",
                RoleType = new Models.RoleType
                {
                    RoleTypeId = 2,
                    Name = "Papel secundário",
                    IconSource = "ic_secondaryRole.png"
                }
            };
            yield return new Models.Role
            {
                Name = "Tank",
                RoleType = new Models.RoleType
                {
                    RoleTypeId = 2,
                    Name = "Papel secundário",
                    IconSource = "ic_secondaryRole.png"
                }
            };
        }

        public Task<Models.Role> GetRole(int roleId)
        {
            return Task.FromResult(Roles.First());
        }

        public Task<IEnumerable<Models.Role>> GetRoleByTypeAsync(int roleTypeId)
        {
            return Task.FromResult(Roles);
        }

        public Task<IEnumerable<Models.Role>> GetRolesAsync()
        {
            return Task.FromResult(Roles);
        }
    }
}
