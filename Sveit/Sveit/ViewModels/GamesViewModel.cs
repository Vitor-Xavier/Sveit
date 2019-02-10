using Sveit.Base.ViewModels;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Game;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class GamesViewModel : BaseViewModel
    {
        private readonly IGameService _gameService;

        private int position = 0;

        public int Position
        {
            get { return position; }
            set { position = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Game> Games { get; set; }

        public IAsyncCommand<Game> GameCommand => new AsyncCommand<Game>(GameCommandExecute);

        public GamesViewModel(IGameService gameService)
        {
            _gameService = gameService;

            Games = new ObservableCollection<Game>();
        }

        public override Task InitializeAsync(params object[] navigationData) => LoadGames();

        private async Task LoadGames()
        {
            if (IsLoading) return;
            if (Games == null) return;
            IsLoading = true;
            var games = await _gameService.GetGamesAsync();

            Device.BeginInvokeOnMainThread(() =>
            {
                Games.Clear();
                foreach (Game game in games)
                    Games.Add(game);
            });
            IsLoading = false;
        }

        public async Task GameCommandExecute(Game game)
        {
            await NavigationService.NavigateToAsync<GameViewModel>(game);
        }
    }
}
