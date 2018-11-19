using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
        private readonly INavigation _navigation;

        private readonly IRequestService _requestService;

        private readonly IPlayerService _playerService;

        private readonly ILoginService _loginService;

        private readonly IGenderService _genderService;

        private readonly IImageService _imageService;

        private Player player;

        public Player Player
        {
            get { return player; }
            set { player = value; OnPropertyChanged(); }
        }

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

        public PlayerRegisterViewModel(INavigation navigation, IRequestService requestService)
        {
            AvatarSource = "https://www.yourfirstpatient.com/assets/default-user-avatar-thumbnail@2x-ad6390912469759cda3106088905fa5bfbadc41532fbaa28237209b1aa976fc9.png";
            _navigation = navigation;
            _requestService = requestService;
            if (AppSettings.ApiStatus)
            {
                _playerService = new PlayerService(_requestService);
                _genderService = new GenderService(_requestService);
                _imageService = new ImageService(_requestService);
                _loginService = new LoginService(_requestService);
            }
            else
            {
                _playerService = new FakePlayerService();
                _loginService = new FakeLoginService();
                _genderService = new FakeGenderService();
                _imageService = new ImageService(new RequestService());
            }
            
            Genders = new ObservableCollection<Gender>();
            Task.Run(() => LoadGenders());
        }

        private async Task LoadGenders()
        {
            var genders = await _genderService.GetGendersAsync();

            Genders.Clear();
            foreach (var g in genders)
                Genders.Add(g);
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
                Username = Username,
                Email = Email,
                Password = Utils.SHA2Utilities.ComputeSha256Hash(Password),
                Name = Name,
                Nickname = Nickname,
                AvatarSource = AvatarSource,
                DateOfBirth = DateOfBirth,
                GenderId = Gender.GenderId,
                Gender = Gender
            };
            var result = await _playerService.PostPlayerAsync(player);
            if (result != null)
            {
                var loggedPlayer = await _loginService.LogIn(player.Username, player.Password);
                await _navigation.PushModalAsync(new ContactsPlayerRegisterPage(_requestService, result));
            }
            else
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
                //TODO: Change file name
                //AvatarSource = await _imageService.PostImage("tst", file.Path);
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
        private bool ContinueCommandCanExecute()
        {
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 4)
                return false;
            if (Username.Any(ch => Char.IsLetterOrDigit(ch)))
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
    }
}
