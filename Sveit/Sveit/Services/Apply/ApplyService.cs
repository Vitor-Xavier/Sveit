using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Login;
using Sveit.Services.Requests;

namespace Sveit.Services.Apply
{
    public class ApplyService : IApplyService
    {
        private readonly IRequestService _requestService;

        private readonly ILoginService _loginService;

        public ApplyService(IRequestService requestService)
        {
            _requestService = requestService;
            _loginService = new LoginService(_requestService);
        }

        public Task<Models.Apply> GetApply(int applyId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.AppliesEndpoint);
            builder.AppendToPath(applyId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<Models.Apply>(uri);
        }

        public Task<IEnumerable<Models.Apply>> GetAppliesByVacancy(int vacancyId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.AppliesEndpoint);
            builder.AppendToPath("Apply/Vacancy/");
            builder.AppendToPath(vacancyId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync< IEnumerable<Models.Apply>>(uri);
        }

        public Task<IEnumerable<Models.Apply>> GetAppliesByPlayer(int playerId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.AppliesEndpoint);
            builder.AppendToPath("Apply/Player/");
            builder.AppendToPath(playerId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Apply>>(uri);
        }

        public async Task<Models.Apply> PostApply(Models.Apply apply)
        {
            UriBuilder builder = new UriBuilder(AppSettings.AppliesEndpoint);
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.PostAsync<Models.Apply, Models.Apply>(uri, apply, token);
        }

        public async Task<bool> DeleteApply(int applyId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.AppliesEndpoint);
            builder.AppendToPath(applyId.ToString());
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.DeleteAsync(uri, token);
        }
    }
}
