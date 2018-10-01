using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Sveit.Models;
using Sveit.Services.Game;
using Sveit.Services.Platform;
using Sveit.Services.Requests;
using Sveit.Services.Team;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    class TeamRegisterViewModel : BindableObject
    {
        private readonly INavigation _navigation;

        private readonly IGameService _gameService;

        private readonly IPlatformService _platformService;

        private readonly ITeamService _teamService;

        private Game game;

        public Game Game
        {
            get { return game; }
            set
            {
                game = value;
                LoadPlatforms(game.GameId);
                OnPropertyChanged();
            }
        }

        private Platform platform;

        public Platform Platform
        {
            get { return platform; }
            set { platform = value; OnPropertyChanged(); }
        }

        private bool hasPlatforms = false;

        public bool HasPlatforms
        {
            get { return hasPlatforms; }
            set { hasPlatforms = value; OnPropertyChanged(); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        private string iconSource;

        public string IconSource
        {
            get { return iconSource; }
            set { iconSource = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Game> Games { get; set; }

        public ObservableCollection<Platform> Platforms { get; set; }

        public ICommand ContinueCommand => new Command(ContinueCommandExecute);

        public ICommand ImageCommand => new Command(GameCommandExecute);

        public ICommand GameCommand => new Command(GameCommandExecute);

        public ICommand PlatformCommand => new Command(PlatformCommandExecute);

        public TeamRegisterViewModel(INavigation navigation)
        {
            _navigation = navigation;
            IRequestService requestService = new RequestService();
            _gameService = new GameService(requestService);
            _platformService = new PlatformService(requestService);
            _teamService = new TeamService(requestService);
            Games = new ObservableCollection<Game>();
            Platforms = new ObservableCollection<Platform>();
            LoadGames();

            IconSource = "http://liquipedia.net/commons/images/thumb/1/1d/Immortals_org.png/600px-Immortals_org.png";
        }

        private async void HandlePlatformSelected(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var item = e.Item as Platform;
            Platform = item;
            await _navigation.PopAllPopupAsync();
        }

        private async void HandleGameSelected(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var item = e.Item as Game;
            Game = item;
            await _navigation.PopAllPopupAsync();
        }

        //private async void LoadGames()
        //{
        //    var games = await _gameService.GetGamesAsync();

        //    Games.Clear();
        //    foreach (Game game in games)
        //        Games.Add(game);
        //    if (Games.Count > 0)
        //        Game = Games[0];
        //}

        private void LoadGames()
        {
            var games = new List<Game>
            {
                new Game { Name = "Overwatch", IconSource = "https://pre00.deviantart.net/29ff/th/pre/f/2017/024/6/c/overwatch_logo_by_stefanthepribic-dawlopl.png", ImageSource = "https://cdnb.artstation.com/p/assets/images/images/011/306/573/large/kevin-kjormo-ana-new-highlight.jpg?1528905182" },
                new Game { Name = "Counter Strike" },
                new Game { Name = "Black Ops 4" },
                new Game { Name = "Battlefield V" }
            };

            Games.Clear();
            foreach (Game game in games)
                Games.Add(game);
            if (Games.Count > 0)
                Game = Games[0];
        }

        //private async void LoadPlatforms(int gameId)
        //{
        //    var platforms = await _platformService.GetPlatformsByGameAsync(gameId);

        //    Platforms.Clear();
        //    foreach (Platform platform in platforms)
        //        Platforms.Add(platform);
        //    HasPlatforms = Platforms.Count > 0;
        //}

        private void LoadPlatforms(int gameId)
        {
            var platforms = new List<Platform>
            {
                new Platform { Name = "Xbox ONE",
                    IconSource = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f9/Xbox_one_logo.svg/1024px-Xbox_one_logo.svg.png" },
                new Platform { Name = "Playstation 4",
                    IconSource = "https://logodownload.org/wp-content/uploads/2017/05/playstation-4-logo-ps4-6.png" },
                new Platform { Name = "PC",
                    IconSource = "https://pre00.deviantart.net/10db/th/pre/i/2017/235/5/4/pc_master_race_by_kingvego-d9r6gtn.png" },
                new Platform { Name = "Switch" }
            };

            Platforms.Clear();
            foreach (Platform platform in platforms)
                Platforms.Add(platform);
            if (Platforms.Count > 0)
                Platform = Platforms[0];
            HasPlatforms = Platforms.Count > 0;
        }

        private async void ContinueCommandExecute()
        {
            if (!CheckFields()) return;
            Team team = new Team
            {
                GamePlatform_GameId = Game.GameId,
                GamePlatform_PlatformId = Platform.PlatformId,
                Name = Name,
                OwnerId = 1,
                Deleted = false,
                IconSource = IconSource
            };

            var created = await _teamService.PostTeam(team);
            if (created != null)
                await App.Current.MainPage.DisplayAlert("Created", "Id: " + created.TeamId, "Ok");
            await App.Current.MainPage.Navigation.PopModalAsync();

        }

        private async void GameCommandExecute()
        {
            var popupPlatform = new Views.PopupPickerPage
            {
                BindingContext = new PopupPickerViewModel<Game>(_navigation, Games)
            };
            (popupPlatform.BindingContext as PopupPickerViewModel<Game>).ItemSelected += HandleGameSelected;
            (popupPlatform.BindingContext as PopupPickerViewModel<Game>).Title = AppResources.Platform;
            await _navigation.PushPopupAsync(popupPlatform);
        }

        private async void PlatformCommandExecute()
        {
            var popupPlatform = new Views.PopupPickerPage
            {
                BindingContext = new PopupPickerViewModel<Platform>(_navigation, Platforms)
            };
            (popupPlatform.BindingContext as PopupPickerViewModel<Platform>).ItemSelected += HandlePlatformSelected;
            (popupPlatform.BindingContext as PopupPickerViewModel<Platform>).Title = AppResources.Platform;
            await _navigation.PushPopupAsync(popupPlatform);
        }

        private bool CheckFields()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return false;
            if (string.IsNullOrWhiteSpace(IconSource))
                return false;
            if (Game == null)
                return false;
            if (Platform == null)
                return false;
            return true;
        }
    }
}
