using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sveit.Services.Platform
{
    public class FakePlatformService : IPlatformService
    {
        public IEnumerable<Models.Platform> Platforms { get; private set; }

        public FakePlatformService()
        {
            Platforms = GetFakePlatforms();
        }

        private IEnumerable<Models.Platform> GetFakePlatforms()
        {
            yield return new Models.Platform
            {
                Name = "Xbox ONE",
                IconSource = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f9/Xbox_one_logo.svg/1024px-Xbox_one_logo.svg.png"
            };
            yield return new Models.Platform
            {
                Name = "Playstation 4",
                IconSource = "https://logodownload.org/wp-content/uploads/2017/05/playstation-4-logo-ps4-6.png"
            };
            yield return new Models.Platform
            {
                Name = "PC",
                IconSource = "https://pre00.deviantart.net/10db/th/pre/i/2017/235/5/4/pc_master_race_by_kingvego-d9r6gtn.png"
            };
            yield return new Models.Platform { Name = "Switch" };
        }

        public Task<Models.Platform> AddPlatformAsync(Models.Platform platform)
        {
            return Task.FromResult(platform);
        }

        public Task<Models.Platform> GetPlatformAsync(int platformId)
        {
            return Task.FromResult(Platforms.First());
        }

        public Task<IEnumerable<Models.Platform>> GetPlatformsAsync()
        {
            return Task.FromResult(Platforms);
        }

        public Task<IEnumerable<Models.Platform>> GetPlatformsByGameAsync(int gameId)
        {
            return Task.FromResult(Platforms);
        }

        public Task<IEnumerable<Models.Platform>> GetPlatformsByNameAsync(string name)
        {
            return Task.FromResult(Platforms);
        }

        public Task<bool> RemovePlatformAsync(int platformId)
        {
            return Task.FromResult(false);
        }
    }
}
