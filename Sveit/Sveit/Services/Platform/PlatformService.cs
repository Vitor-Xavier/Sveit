using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sveit.Extensions;
using Sveit.Services.Login;
using Sveit.Services.Requests;

namespace Sveit.Services.Platform
{
    class PlatformService : IPlatformService
    {
        private readonly IRequestService _requestService;

        private readonly ILoginService _loginService;

        public PlatformService(IRequestService requestService)
        {
            _requestService = requestService;
            _loginService = new LoginService(_requestService);
        }

        public Task<Models.Platform> GetPlatformAsync(int platformId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlatformsEndpoint);
            builder.AppendToPath(platformId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<Models.Platform>(uri);
        }

        public Task<IEnumerable<Models.Platform>> GetPlatformsAsync()
        {
            string uri = AppSettings.PlatformsEndpoint;

            return _requestService.GetAsync<IEnumerable<Models.Platform>>(uri);
        }

        public Task<IEnumerable<Models.Platform>> GetPlatformsByGameAsync(int gameId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlatformsEndpoint);
            builder.AppendToPath("Game");
            builder.AppendToPath(gameId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Platform>>(uri);
        }

        public Task<IEnumerable<Models.Platform>> GetPlatformsByNameAsync(string name)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlatformsEndpoint);
            builder.AppendToPath("Name");
            builder.AppendToPath(name);
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Platform>>(uri);
        }

        public async Task<Models.Platform> AddPlatformAsync(Models.Platform platform)
        {
            string uri = AppSettings.PlatformsEndpoint;

            var token = await _loginService.GetOAuthToken();
            return await _requestService.PostAsync<Models.Platform, Models.Platform>(uri, platform, token);
        }

        public async Task<bool> RemovePlatformAsync(int platformId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlatformsEndpoint);
            builder.AppendToPath(platformId.ToString());
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.DeleteAsync(uri, token);
        }
    }
}
