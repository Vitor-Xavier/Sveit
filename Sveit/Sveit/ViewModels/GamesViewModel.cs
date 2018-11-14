using Sveit.Extensions;
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
        private readonly INavigation _navigation;

        private readonly IGameService _gameService;

        private readonly IRequestService _requestService;

        private int position = 0;

        public int Position
        {
            get { return position; }
            set { position = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Game> Games { get; set; }

        public IAsyncCommand<Game> GameCommand => new AsyncCommand<Game>(GameCommandExecute);

        public GamesViewModel(INavigation navigation, IRequestService requestService)
        {
            _navigation = navigation;
            _requestService = requestService;
            if (AppSettings.ApiStatus)
                _gameService = new GameService(requestService);
            else
                _gameService = new FakeGameService();

            Games = new ObservableCollection<Game>();
            Task.Run(() => LoadGames());
        }

        private async Task LoadGames()
        {
            if (IsLoading) return;
            IsLoading = true;
            var games = await _gameService.GetGamesAsync();

            Games.Clear();
            foreach (Game g in games)
                Games.Add(g);
            IsLoading = false;
        }

        public async Task GameCommandExecute(Game game)
        {
            await _navigation.PushAsync(new Views.GamePage(_requestService, game));
        }
    }
}
