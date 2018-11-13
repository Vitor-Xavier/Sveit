using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Login;
using Sveit.Services.Requests;

namespace Sveit.Services.Vacancy
{
    public class VacancyService : IVacancyService
    {
        private readonly IRequestService _requestService;

        private readonly ILoginService _loginService;

        public VacancyService(IRequestService requestService)
        {
            _requestService = requestService;
            _loginService = new LoginService(_requestService);
        }

        public Task<Models.Vacancy> GetVacancyAsync(int vacancyId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.VacanciesEndpoint);
            builder.AppendToPath(vacancyId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<Models.Vacancy>(uri);
        }

        public Task<IEnumerable<Models.Vacancy>> GetVacanciesByGameAsync(int gameId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.VacanciesEndpoint);
            builder.AppendToPath("Game");
            builder.AppendToPath(gameId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Vacancy>>(uri);
        }

        public Task<IEnumerable<Models.Vacancy>> GetVacanciesByTeamAsync(int teamId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.VacanciesEndpoint);
            builder.AppendToPath("Team");
            builder.AppendToPath(teamId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Vacancy>>(uri);
        }

        public async Task<Models.Vacancy> PostVacancyAsync(Models.Vacancy vacancy)
        {
            UriBuilder builder = new UriBuilder(AppSettings.VacanciesEndpoint);
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.PostAsync<Models.Vacancy, Models.Vacancy>(uri, vacancy, token);
        }

        public async Task<bool> DeleteVacancyAsync(int vacancyId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.VacanciesEndpoint);
            builder.AppendToPath(vacancyId.ToString());
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.DeleteAsync(uri, token);
        }
    }
}
