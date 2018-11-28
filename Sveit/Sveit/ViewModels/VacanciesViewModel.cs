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
        private readonly INavigation _navigation;

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

        public IAsyncCommand<Vacancy> VacancyCommand => new AsyncCommand<Vacancy>(VacancyCommandExecute);

        public VacanciesViewModel(INavigation navigation, IRequestService requestService)
        {
            _navigation = navigation;
            _requestService = requestService;
            if (AppSettings.ApiStatus)
            {
                _vacancyService = new VacancyService(requestService);
                _gameService = new GameService(requestService);
            }
            else
            {
                _vacancyService = new FakeVacancyService();
                _gameService = new FakeGameService();
            }

            Games = new ObservableCollection<Game>();
            Vacancies = new ObservableCollection<Vacancy>();
            Task.Run(() => LoadGames());
        }

        private async Task LoadByGame(int pos)
        {
            var vacancies = await _vacancyService.GetVacanciesByGameAsync(Games[pos].GameId);
            Vacancies.Clear();
            foreach (Vacancy vacancy in vacancies)
                Vacancies.Add(vacancy);
        }

        private async Task LoadGames()
        {
            if (IsLoading) return;
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
            await _navigation.PushModalAsync(new Views.VacancyPage(_requestService, vacancy));
        }
    }
}
