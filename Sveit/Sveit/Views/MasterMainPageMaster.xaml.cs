
using Sveit.Models;
using Sveit.Services.Login;
using Sveit.Services.Requests;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMainPageMaster : ContentPage
    {
        public ListView ListView;

        public MasterMainPageMaster()
        {
            InitializeComponent();

            BindingContext = new MasterMainPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        public class MasterMainPageMasterViewModel : INotifyPropertyChanged
        {
            private ILoginService _loginService;

            public ObservableCollection<MasterMenuItem> MenuItems { get; set; }

            private Player loggedPlayer;

            public Player LoggedPlayer
            {
                get { return loggedPlayer; }
                set { loggedPlayer = value; OnPropertyChanged(); }
            }

            public MasterMainPageMasterViewModel()
            {
                if (AppSettings.ApiStatus)
                    _loginService = new LoginService(new RequestService());
                else
                    _loginService = new FakeLoginService();
                LoadItems();
            }

            private async void LoadItems()
            {
                LoggedPlayer = await _loginService.CheckLogIn();

                if (LoggedPlayer == null)
                {
                    MenuItems = new ObservableCollection<MasterMenuItem>(new[]
                    {
                        new MasterMenuItem { Id = 0, Title = AppResources.LogIn, Icon = "baseline_settings_white_24", TargetType = typeof(LoginPage), TransparentNavBar = true },
                        new MasterMenuItem { Id = 1, Title = AppResources.News, Icon = "baseline_fiber_new_white_24", TargetType = typeof(HomePage) },
                        new MasterMenuItem { Id = 3, Title = AppResources.Games, Icon = "baseline_gamepad_white_24", TargetType = typeof(GamesPage), TransparentNavBar = true },
                        new MasterMenuItem { Id = 4, Title = AppResources.Vacancies, Icon = "baseline_work_white_24", TargetType = typeof(VacanciesPage), TransparentNavBar = true },
                        new MasterMenuItem { Id = 5, Title = AppResources.Settings, Icon = "baseline_settings_white_24", TargetType = typeof(SettingsPage) }
                    });
                }
                else
                {
                    MenuItems = new ObservableCollection<MasterMenuItem>(new[]
                    {
                        new MasterMenuItem { Id = 1, Title = AppResources.News, Icon = "baseline_fiber_new_white_24", TargetType = typeof(HomePage) },
                        new MasterMenuItem { Id = 2, Title = AppResources.Profile, Icon = "baseline_account_box_white_24", TargetType = typeof(PlayerPage), TransparentNavBar = true },
                        new MasterMenuItem { Id = 3, Title = AppResources.Games, Icon = "baseline_gamepad_white_24", TargetType = typeof(GamesPage), TransparentNavBar = true },
                        new MasterMenuItem { Id = 4, Title = AppResources.Vacancies, Icon = "baseline_work_white_24", TargetType = typeof(VacanciesPage), TransparentNavBar = true },
                        new MasterMenuItem { Id = 5, Title = AppResources.Settings, Icon = "baseline_settings_white_24", TargetType = typeof(SettingsPage) },
                        new MasterMenuItem { Id = 6, Title = "Exit", Icon = "baseline_settings_white_24" },
                });
                }
                
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}