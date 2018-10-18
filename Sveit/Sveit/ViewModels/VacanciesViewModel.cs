using Sveit.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Sveit.Services.Vacancy;
using Sveit.Services.Requests;
using Sveit.Services.Game;

namespace Sveit.ViewModels
{
    public class VacanciesViewModel : BindableObject
    {
        public ObservableCollection<Vacancy> Vacancies { get; set; }

        public ObservableCollection<Game> Games { get; set; }

        private readonly INavigation _navigation;

        private readonly IVacancyService _vacancyService;

        private readonly IGameService _gameService;

        private int position = 0;

        public int Position
        {
            get { return position; }
            set
            {
                position = value;
                LoadByGame(position);
                OnPropertyChanged();
            }
        }

        public VacanciesViewModel(INavigation navigation)
        {
            _navigation = navigation;
            if (AppSettings.ApiStatus)
            {
                var requestService = new RequestService();
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
            LoadGames();
        }

        private async void LoadByGame(int pos)
        {
            var vacancies = await _vacancyService.GetVacanciesByGameAsync(Games[pos].GameId);
            Vacancies.Clear();
            foreach (Vacancy vacancy in vacancies)
                Vacancies.Add(vacancy);
        }

        private async void LoadGames()
        {
            var games = await _gameService.GetTrendGamesAsync();

            Games.Clear();
            foreach (Game game in games)
                Games.Add(game);

            if (Games.Count > 0)
                Position = 0;
        }

        public async void VacancySelectedCommandExecute(Vacancy vacancy)
        {
            await _navigation.PushModalAsync(new Views.VacancyPage(vacancy));
        }
    }
}
