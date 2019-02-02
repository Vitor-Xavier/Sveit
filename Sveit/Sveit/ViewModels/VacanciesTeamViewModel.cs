using Sveit.Base.ViewModels;
using Sveit.Controls;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Requests;
using Sveit.Services.Vacancy;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class VacanciesTeamViewModel : BaseViewModel
    {
        private readonly int _teamId;

        private readonly INavigation _navigation;

        private readonly IRequestService _requestService;

        private readonly IVacancyService _vacancyService;

        public ObservableCollection<Vacancy> Vacancies { get; set; }

        public IAsyncCommand<Vacancy> VacancyCommand => new AsyncCommand<Vacancy>(VacancyCommandExecute);

        public IAsyncCommand<Vacancy> UpdateVacancyCommand => new AsyncCommand<Vacancy>(UpdateVacancyCommandExecute);

        public IAsyncCommand<Vacancy> CloseVacancyCommand => new AsyncCommand<Vacancy>(CloseVacancyCommandExecute);

        public VacanciesTeamViewModel(INavigation navigation, IRequestService requestService, int teamId)
        {
            _navigation = navigation;
            _teamId = teamId;
            _requestService = requestService;
            if (AppSettings.ApiStatus)
                _vacancyService = new VacancyService(_requestService);
            else
                _vacancyService = new FakeVacancyService();
            Vacancies = new ObservableCollection<Vacancy>();

            Task.Run(() => LoadVacancies());
        }

        public async Task LoadVacancies()
        {
            var vacancies = await _vacancyService.GetVacanciesByTeamAsync(_teamId);

            Vacancies.Clear();
            foreach (Vacancy v in vacancies)
                Vacancies.Add(v);
        }

        public async Task VacancyCommandExecute(Vacancy vacancy)
        {
            await _navigation.PushAsync(new Views.AppliesTeamPage(_requestService, vacancy));
        }

        private async Task CloseVacancyCommandExecute(Vacancy vacancy)
        {
            try
            {
                vacancy.Available = false;
                vacancy.Deleted = true;
                var result = await _vacancyService.PostVacancyAsync(vacancy);
                if (result == null)
                    DependencyService.Get<IMessage>().ShortAlert(AppResources.VacancyFailed);
                await LoadVacancies();
            }
            catch
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.VacancyFailed);
            }
        }

        public async Task UpdateVacancyCommandExecute(Vacancy vacancy)
        {
            await _navigation.PushAsync(new Views.VacancyRegisterPage(_requestService, vacancy.Team, vacancy.VacancyId));
        }
    }
}
