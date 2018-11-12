using Sveit.Models;
using Sveit.Services.Game;
using Sveit.Services.Requests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class GamesViewModel : BaseViewModel
    {
        public ObservableCollection<Game> Games { get; set; }

        private readonly INavigation _navigation;

        private readonly IGameService _gameService;

        public GamesViewModel(INavigation navigation, IRequestService requestService)
        {
            _navigation = navigation;
            if (AppSettings.ApiStatus)
                _gameService = new GameService(requestService);
            else
                _gameService = new FakeGameService();

            Games = new ObservableCollection<Game>();
            LoadData();
        }

        private async void LoadData()
        {
            if (IsLoading) return;
            IsLoading = true;
            var games = await _gameService.GetGamesAsync();

            Games.Clear();
            foreach (Game g in games)
                Games.Add(g);
            IsLoading = false;
        }

        public async void GameSelectedCommandExecute(Game game)
        {
            await _navigation.PushAsync(new Views.GamePage(game));
        }
    }
}
