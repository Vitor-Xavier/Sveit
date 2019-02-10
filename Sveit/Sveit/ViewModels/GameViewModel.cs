using Sveit.Base.ViewModels;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Vacancy;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Sveit.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private readonly IVacancyService _vacancyService;

        private Game game;

        public Game Game
        {
            get { return game; }
            set { game = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Vacancy> Vacancies { get; }

        public IAsyncCommand<Vacancy> VacancyCommand => new AsyncCommand<Vacancy>(VacancyCommandExecute);

        public GameViewModel(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;

            Vacancies = new ObservableCollection<Vacancy>();
        }

        public override Task InitializeAsync(params object[] navigationData)
        {
            Game = navigationData[0] as Game;
            return LoadVacancies();
        }

        private async Task LoadVacancies()
        {
            if (IsLoading) return;
            IsLoading = true;
            var vacancies = await _vacancyService.GetVacanciesByGameAsync(Game.GameId);

            Vacancies.Clear();
            foreach (Vacancy v in vacancies)
                Vacancies.Add(v);
            IsLoading = false;
        }

        public async Task VacancyCommandExecute(Vacancy vacancy)
        {
            await NavigationService.NavigateToAsync<VacancyViewModel>(vacancy);
        }
    }
}
