using Sveit.Models;
using Sveit.Services.Gender;
using Sveit.Services.Requests;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    class PlayerRegisterViewModel : BindableObject
    {
        private readonly INavigation _navigation;

        private readonly IRequestService _requestService;

        private readonly IGenderService _genderService;

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

        public ObservableCollection<Gender> Genders { get; set; }

        public PlayerRegisterViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _requestService = new RequestService();
            _genderService = new GenderService(_requestService);
            Genders = new ObservableCollection<Gender>();
            LoadGenders();
        }

        private async void LoadGenders()
        {
            var genders = await _genderService.GetGendersAsync();

            Genders.Clear();
            foreach (var g in genders)
                Genders.Add(g);
        }

    }
}
