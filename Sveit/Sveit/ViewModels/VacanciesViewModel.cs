using Sveit.Base.ViewModels;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Game;
using Sveit.Services.Requests;
using Sveit.Services.Vacancy;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class VacanciesViewModel : BaseViewModel
    {
        private readonly IRequestService _requestService;

        private readonly IVacancyService _vacancyService;

        private readonly IGameService _gameService;

        private int position;

        public int Position
        {
            get { return position; }
            set
            {
                position = value;
                Task.Run(() => LoadByGame(position));
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Vacancy> Vacancies { get; set; }

        public ObservableCollection<Game> Games { get; set; }

        public IAsyncCommand RefreshCommand => new AsyncCommand(RefreshCommandExecute);

        public IAsyncCommand<Vacancy> VacancyCommand => new AsyncCommand<Vacancy>(VacancyCommandExecute);

        public VacanciesViewModel(IVacancyService vacancyService, IGameService gameService)
        {
            _vacancyService = vacancyService;
            _gameService = gameService;

            Games = new ObservableCollection<Game>();
            Vacancies = new ObservableCollection<Vacancy>();
            Task.Run(() => LoadGames());
        }

        private async Task RefreshCommandExecute()
        {
            await LoadByGame(Position);
        }

        private async Task LoadByGame(int pos)
        {
            if (IsLoading) return;
            IsLoading = true;
            try
            {
                var vacancies = await _vacancyService.GetVacanciesByGameAsync(Games[pos].GameId);
                Vacancies.Clear();
                foreach (Vacancy vacancy in vacancies)
                    Vacancies.Add(vacancy);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task LoadGames()
        {
            if (IsLoading) return;
            if (Games == null) return;
            IsLoading = true;
            var games = await _gameService.GetTrendGamesAsync();

            Device.BeginInvokeOnMainThread(() => 
            {
                Games.Clear();
                foreach (Game game in games)
                    Games.Add(game);
            });
            IsLoading = false;
        }

        public async Task VacancyCommandExecute(Vacancy vacancy)
        {
            await NavigationService.NavigateToAsync<VacancyViewModel>(vacancy);
            //await _navigation.PushModalAsync(new Views.VacancyPage(_requestService, vacancy));
        }
    }
}
