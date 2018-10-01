using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.Services.Platform
{
    interface IPlatformService
    {
        Task<Models.Platform> GetPlatformAsync(int platformId);

        Task<IEnumerable<Models.Platform>> GetPlatformsAsync();

        Task<IEnumerable<Models.Platform>> GetPlatformsByNameAsync(string name);

        Task<IEnumerable<Models.Platform>> GetPlatformsByGameAsync(int gameId);

        Task<bool> AddPlatformAsync(Models.Platform platform);

        Task<bool> RemovePlatformAsync(int platformId);
    }
}
