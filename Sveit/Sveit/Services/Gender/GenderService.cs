using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Login;
using Sveit.Services.Requests;

namespace Sveit.Services.Gender
{
    class GenderService : IGenderService
    {
        private readonly IRequestService _requestService;

        private readonly ILoginService _loginService;

        public GenderService(IRequestService requestService)
        {
            _requestService = requestService;
            _loginService = new LoginService(_requestService);
        }

        public Task<Models.Gender> GetGenderAsync(int genderId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.GendersEndpoint);
            builder.AppendToPath(genderId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<Models.Gender>(uri);
        }

        public Task<IEnumerable<Models.Gender>> GetGendersAsync()
        {
            string uri = AppSettings.GendersEndpoint;

            return _requestService.GetAsync<IEnumerable<Models.Gender>>(uri);
        }

        public Task<IEnumerable<Models.Gender>> GetGendersByNameAsync(string name)
        {
            UriBuilder builder = new UriBuilder(AppSettings.GendersEndpoint);
            builder.AppendToPath("Name");
            builder.AppendToPath(name);
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Gender>>(uri);
        }

        public async Task<bool> AddGenderAsync(Models.Gender gender)
        {
            string uri = AppSettings.GendersEndpoint;

            var token = await _loginService.GetOAuthToken();
            return await _requestService.PostAsync<Models.Gender, bool>(uri, gender, token);
        }

        public async Task<bool> RemoveGenderAsync(int genderId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.GendersEndpoint);
            builder.AppendToPath(genderId.ToString());
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.DeleteAsync(uri, token);
        }
    }
}
