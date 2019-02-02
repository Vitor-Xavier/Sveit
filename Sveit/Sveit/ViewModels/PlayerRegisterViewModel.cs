using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Sveit.Base.ViewModels;
using Sveit.Controls;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Gender;
using Sveit.Services.Image;
using Sveit.Services.Login;
using Sveit.Services.Player;
using Sveit.Services.Requests;
using Sveit.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    class PlayerRegisterViewModel : BaseViewModel
    {
        private readonly int _playerId;

        private readonly INavigation _navigation;

        private readonly IRequestService _requestService;

        private readonly IPlayerService _playerService;

        private readonly ILoginService _loginService;

        private readonly IGenderService _genderService;

        private readonly IImageService _imageService;

        private Gender gender;

        public Gender Gender
        {
            get { return gender; }
            set { gender = value; OnPropertyChanged(); }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; OnPropertyChanged(); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        private string avatarSource;

        public string AvatarSource
        {
            get { return avatarSource; }
            set { avatarSource = value; OnPropertyChanged(); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        private string nickname;

        public string Nickname
        {
            get { return nickname; }
            set { nickname = value; OnPropertyChanged(); }
        }

        private DateTime dateOfBirth;

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Gender> Genders { get; set; }

        public IAsyncCommand ImageCommand => new AsyncCommand(ImageCommandExecute);

        public IAsyncCommand ContinueCommand => new AsyncCommand(ContinueCommandExecute);

        public IAsyncCommand GenderCommand => new AsyncCommand(GenderCommandExecute);

        public PlayerRegisterViewModel(INavigation navigation, IRequestService requestService, int playerId)
        {
            _playerId = playerId;
            AvatarSource = "https://www.yourfirstpatient.com/assets/default-user-avatar-thumbnail@2x-ad6390912469759cda3106088905fa5bfbadc41532fbaa28237209b1aa976fc9.png";
            _navigation = navigation;
            _requestService = requestService;
            if (AppSettings.ApiStatus)
            {
                _playerService = new PlayerService(new RequestService());
                _loginService = new LoginService(_requestService);
                _genderService = new GenderService(new RequestService());
                _imageService = new ImageService(_requestService);
            }
            else
            {
                _playerService = new FakePlayerService();
                _loginService = new FakeLoginService();
                _genderService = new FakeGenderService();
                _imageService = new ImageService(new RequestService());
            }
            DateOfBirth = DateTime.Today.AddYears(-16);
            Genders = new ObservableCollection<Gender>();
            Task.Run(() => LoadGenders());
            Task.Run(() => LoadPlayer());
        }

        private async Task GenderCommandExecute()
        {
            await LoadGenders();
        }

        private async Task LoadGenders()
        {
            try
            {
                var genders = await _genderService.GetGendersAsync();

                Device.BeginInvokeOnMainThread(() =>
                {
                    Genders.Clear();
                    foreach (var g in genders)
                        Genders.Add(g);
                });
            }
            catch (Exception e)
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.Gender);
            }
        }

        private async Task ContinueCommandExecute()
        {
            if (!ContinueCommandCanExecute())
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.InvalidValues);
                return;
            }
            var player = new Player
            {
                PlayerId = _playerId,
                Username = Username,
                Email = Email,
                Password = Utils.SHA2Utilities.ComputeSha256Hash(Password),
                Name = Name,
                Nickname = Nickname,
                AvatarSource = AvatarSource,
                DateOfBirth = DateOfBirth,
                GenderId = Gender.GenderId
            };
            try
            {
                Player result;
                if (player.PlayerId == 0)
                    result = await _playerService.PostPlayerAsync(player);
                else
                    result = await _playerService.UpdatePlayer(player);
                if (result != null)
                {
                    var loggedPlayer = await _loginService.LogIn(player.Username, player.Password);
                    await _navigation.PushModalAsync(new ContactsPlayerRegisterPage(_requestService, result));
                    
                }
                else
                    DependencyService.Get<IMessage>().ShortAlert(AppResources.PlayerFailed);
            } 
            catch (Exception)
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.PlayerFailed);
            }
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

                try
                {
                    AvatarSource = await _imageService.PostImage(Guid.NewGuid().ToString("N"), file.Path);
                }
                catch
                {
                    DependencyService.Get<IMessage>().ShortAlert(AppResources.ImageFailed);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(AppResources.PermissionsDenied, AppResources.CameraDenied, AppResources.Ok);
                CrossPermissions.Current.OpenAppSettings();
            }
        }
        private bool ContinueCommandCanExecute()
        {
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 4)
                return false;
            if (Username.Any(ch => !Char.IsLetterOrDigit(ch)))
                return false;
            if (string.IsNullOrWhiteSpace(Email) || Email.Length < 5)
                return false;
            if (string.IsNullOrWhiteSpace(Password) || Password.Length < 5)
                return false;
            if (string.IsNullOrWhiteSpace(Name) || Name.Length < 5)
                return false;
            if (string.IsNullOrWhiteSpace(Nickname) || Nickname.Length < 5)
                return false;
            if (string.IsNullOrWhiteSpace(AvatarSource))
                return false;
            if (DateOfBirth == null ||
                DateOfBirth.Year <= 1950 ||
                DateOfBirth.Year >= DateTime.Now.AddYears(-12).Year)
                return false;
            if (Gender == null)
                return false;
            return true;
        }

        private async Task LoadPlayer()
        {
            if (_playerId == 0) return;

            var player = await _playerService.GetPlayerById(_playerId);
            if (player == null) return;

            Name = player.Name;
            Nickname = player.Nickname;
            AvatarSource = player.AvatarSource;
            DateOfBirth = player.DateOfBirth;
            if (Genders?.Count > 0)
                Gender = Genders.Where(g => g.GenderId == player.Gender.GenderId).FirstOrDefault();
        }
    }
}
