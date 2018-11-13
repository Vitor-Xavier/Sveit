using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Requests;

namespace Sveit.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly IRequestService _requestService;

        public RoleService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<Models.Role> GetRole(int roleId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.RolesEndpoint);
            builder.AppendToPath(roleId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<Models.Role>(uri);
        }

        public Task<IEnumerable<Models.Role>> GetRoleByTypeAsync(int roleTypeId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.RolesEndpoint);
            builder.AppendToPath("Type");
            builder.AppendToPath(roleTypeId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Role>>(uri);
        }

        public Task<IEnumerable<Models.Role>> GetRolesAsync()
        {
            string uri = AppSettings.RolesEndpoint;

            return _requestService.GetAsync<IEnumerable<Models.Role>>(uri);
        }
    }
}
