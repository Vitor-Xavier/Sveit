using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Rg.Plugins.Popup.Extensions;
using Sveit.Controls;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Game;
using Sveit.Services.Image;
using Sveit.Services.Platform;
using Sveit.Services.Requests;
using Sveit.Services.Team;
using Sveit.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class TeamRegisterViewModel : BaseViewModel
    {
        private readonly int _teamId;

        private readonly INavigation _navigation;

        private readonly IRequestService _requestService;

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
                Task.Run(() => LoadPlatforms(game.GameId));
                OnPropertyChanged();
            }
        }

        private Platform platform;

        public Platform Platform
        {
            get { return platform; }
            set { platform = value; OnPropertyChanged(); }
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

        public IAsyncCommand ContinueCommand => new AsyncCommand(ContinueCommandExecute);

        public IAsyncCommand ImageCommand => new AsyncCommand(ImageCommandExecute);

        public IAsyncCommand GameCommand => new AsyncCommand(GameCommandExecute);

        public IAsyncCommand PlatformCommand => new AsyncCommand(PlatformCommandExecute);

        public TeamRegisterViewModel(INavigation navigation, IRequestService requestService, int teamId)
        {
            _teamId = teamId;
            _navigation = navigation;
            _requestService = requestService;
            if (AppSettings.ApiStatus)
            {
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
            IconSource = "http://liquipedia.net/commons/images/thumb/1/1d/Immortals_org.png/600px-Immortals_org.png";

            Task.Run(() => LoadGames());
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

        private async Task LoadGames()
        {
            var games = await _gameService.GetGamesAsync();

            Games.Clear();
            foreach (Game game in games)
                Games.Add(game);
            Game = Games.FirstOrDefault();
        }

        private async Task LoadPlatforms(int gameId)
        {
            var platforms = await _platformService.GetPlatformsByGameAsync(gameId);

            Platforms.Clear();
            foreach (Platform platform in platforms)
                Platforms.Add(platform);
            Platform = Platforms.FirstOrDefault();
        }

        private async Task ContinueCommandExecute()
        {
            if (!ContinueCommandCanExecute())
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.InvalidValues);
                return;
            }
            Team team = new Team
            {
                TeamId = _teamId,
                GamePlatform_GameId = Game.GameId,
                GamePlatform_PlatformId = Platform.PlatformId,
                Name = Name,
                OwnerId = App.LoggedPlayer.PlayerId,
                Deleted = false,
                IconSource = IconSource
            };

            try
            {
                var created = await _teamService.PostTeam(team);
                if (created != null)
                {
                    await _navigation.PushModalAsync(new ContactsTeamRegisterPage(_requestService, created));
                }
                else
                    DependencyService.Get<IMessage>().ShortAlert(AppResources.TeamFailed);
            }
            catch
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.TeamFailed);
            }
        }

        private bool ContinueCommandCanExecute()
        {
            if (App.LoggedPlayer == null)
                return false;
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

        private async Task GameCommandExecute()
        {
            var popupPlatform = new PopupPickerPage
            {
                BindingContext = new PopupPickerViewModel<Game>(_navigation, Games)
            };
            (popupPlatform.BindingContext as PopupPickerViewModel<Game>).ItemSelected += HandleGameSelected;
            (popupPlatform.BindingContext as PopupPickerViewModel<Game>).Title = AppResources.Platform;
            await _navigation.PushPopupAsync(popupPlatform);
        }

        private async Task PlatformCommandExecute()
        {
            var popupPlatform = new PopupPickerPage
            {
                BindingContext = new PopupPickerViewModel<Platform>(_navigation, Platforms)
            };
            (popupPlatform.BindingContext as PopupPickerViewModel<Platform>).ItemSelected += HandlePlatformSelected;
            (popupPlatform.BindingContext as PopupPickerViewModel<Platform>).Title = AppResources.Platform;
            await _navigation.PushPopupAsync(popupPlatform);
        }

        private async Task ImageCommandExecute()
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
                    await App.Current.MainPage.DisplayAlert(AppResources.NotSupported, AppResources.GalleryNotSupported, AppResources.Ok);
                    return;
                }
                var file = await CrossMedia.Current.PickPhotoAsync();
                if (file == null) return;
                //TODO: Change file name
                //IconSource = await _imageService.PostImage("tst", file.Path);
                //var image = ImageSource.FromStream(() =>
                //{
                //    var stream = file.GetStream();
                //    file.Dispose();
                //    return stream;
                //});

            }
            else
            {
                await App.Current.MainPage.DisplayAlert(AppResources.PermissionsDenied, AppResources.CameraDenied, AppResources.Ok);
                CrossPermissions.Current.OpenAppSettings();
            }
        }

        private async Task LoadTeam()
        {
            if (_teamId == 0) return;

            var team = await _teamService.GetById(_teamId);
            if (team == null) return;

            Name = team.Name;
            IconSource = team.IconSource;
            Game = Games.Where(g => team.GamePlatform_GameId == g.GameId).FirstOrDefault();
            Platform = Platforms.Where(p => team.GamePlatform_PlatformId == p.PlatformId).FirstOrDefault();
        }
    }
}
