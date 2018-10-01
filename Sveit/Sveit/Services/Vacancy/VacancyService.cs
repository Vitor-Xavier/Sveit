using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Requests;

namespace Sveit.Services.Vacancy
{
    public class VacancyService : IVacancyService
    {
        private readonly IRequestService _requestService;

        public VacancyService(IRequestService requestService)
        {
            _requestService = requestService;
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

        public Task<bool> PostVacancyAsync(Models.Vacancy vacancy)
        {
            UriBuilder builder = new UriBuilder(AppSettings.VacanciesEndpoint);
            string uri = builder.ToString();

            return _requestService.PostAsync<Models.Vacancy, bool>(uri, vacancy);
        }

        public Task<bool> DeleteVacancyAsync(int vacancyId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.VacanciesEndpoint);
            builder.AppendToPath(vacancyId.ToString());
            string uri = builder.ToString();

            return _requestService.DeleteAsync(uri, "");
        }
    }
}
