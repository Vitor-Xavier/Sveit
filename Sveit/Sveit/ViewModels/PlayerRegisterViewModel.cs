using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Gender;
using Sveit.Services.Image;
using Sveit.Services.Requests;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    class PlayerRegisterViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly IRequestService _requestService;

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

        public PlayerRegisterViewModel(INavigation navigation)
        {
            AvatarSource = "https://www.yourfirstpatient.com/assets/default-user-avatar-thumbnail@2x-ad6390912469759cda3106088905fa5bfbadc41532fbaa28237209b1aa976fc9.png";
            _navigation = navigation;
            if (AppSettings.ApiStatus)
            {
                _requestService = new RequestService();
                _genderService = new GenderService(_requestService);
                _imageService = new ImageService(_requestService);
            }
            else
            {
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
    }
}
