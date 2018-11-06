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
using Sveit.Views;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using Plugin.Media;
using Sveit.Services.Image;

namespace Sveit.ViewModels
{
    class TeamRegisterViewModel : BindableObject
    {
        private readonly INavigation _navigation;

        private readonly IGameService _gameService;

        private readonly IPlatformService _platformService;

        private readonly ITeamService _teamService;

        private readonly IImageService _imageService;

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

        public ICommand ImageCommand => new Command(ImageCommandExecute);

        public ICommand GameCommand => new Command(GameCommandExecute);

        public ICommand PlatformCommand => new Command(PlatformCommandExecute);

        public TeamRegisterViewModel(INavigation navigation)
        {
            _navigation = navigation;
            if (AppSettings.ApiStatus)
            {
                IRequestService requestService = new RequestService();
                _gameService = new GameService(requestService);
                _platformService = new PlatformService(requestService);
                _teamService = new TeamService(requestService);
                _imageService = new ImageService(requestService);
            }
            else
            {
                _gameService = new FakeGameService();
                _platformService = new FakePlatformService();
                _teamService = new FakeTeamService();
                _imageService = new ImageService(new RequestService());
            }
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

        private async void LoadGames()
        {
            var games = await _gameService.GetGamesAsync();

            Games.Clear();
            foreach (Game game in games)
                Games.Add(game);
            if (Games.Count > 0)
                Game = Games[0];
        }

        private async void LoadPlatforms(int gameId)
        {
            var platforms = await _platformService.GetPlatformsByGameAsync(gameId);

            Platforms.Clear();
            foreach (Platform platform in platforms)
                Platforms.Add(platform);
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
            {
                await _navigation.PushModalAsync(new ContactsTeamRegisterPage(created));
            }
            else
                await _navigation.PopModalAsync();

        }

        private async void ImageCommandExecute()
        {
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage });
                storageStatus = results[Permission.Storage];
            }

            if (storageStatus == PermissionStatus.Granted)
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("Not supported", "Gallery not supported", "OK");
                    return;
                }
                var file = await CrossMedia.Current.PickPhotoAsync();
                if (file == null) return;
                //TODO: Change file name
                IconSource = await _imageService.PostImage("tst", file.Path);
                //var image = ImageSource.FromStream(() =>
                //{
                //    var stream = file.GetStream();
                //    file.Dispose();
                //    return stream;
                //});

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Permissions Denied", "Unable to take photos.", "OK");
                //On iOS you may want to send your user to the settings screen.
                //CrossPermissions.Current.OpenAppSettings();
            }
        }

        private async void GameCommandExecute()
        {
            var popupPlatform = new PopupPickerPage
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
