using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.Services.Vacancy
{
    interface IVacancyService
    {
        Task<Models.Vacancy> GetVacancyAsync(int vacancyId);

        Task<IEnumerable<Models.Vacancy>> GetVacanciesByTeamAsync(int teamId);

        Task<IEnumerable<Models.Vacancy>> GetVacanciesByGameAsync(int gameId);

        Task<bool> PostVacancyAsync(Models.Vacancy vacancy);

        Task<bool> DeleteVacancyAsync(int vacancyId);
    }
}
