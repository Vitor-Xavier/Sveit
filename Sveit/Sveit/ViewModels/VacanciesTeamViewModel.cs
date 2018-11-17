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
    }
}
