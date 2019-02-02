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

        public GameViewModel(IVacancyService vacancyService, Game game)
        {
            _vacancyService = vacancyService;
            Game = game;

            Vacancies = new ObservableCollection<Vacancy>();
            Task.Run(() => LoadVacancies());
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
            //await _navigation.PushModalAsync(new Views.VacancyPage(_requestService, vacancy));
        }
    }
}
