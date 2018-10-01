using Sveit.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

        class MasterMainPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterMenuItem> MenuItems { get; set; }

            public MasterMainPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterMenuItem>(new[]
                {
                    new MasterMenuItem { Id = 0, Title = AppResources.News, Icon = "baseline_fiber_new_white_24", TargetType = typeof(HomePage) },
                    new MasterMenuItem { Id = 1, Title = AppResources.Games, Icon = "baseline_gamepad_white_24", TargetType = typeof(GamesPage), TransparentNavBar = true },
                    //new MasterMenuItem { Id = 2, Title = "Gêneros", Icon = "baseline_gamepad_white_24", TargetType = typeof(GenrePage) },
                    new MasterMenuItem { Id = 3, Title = AppResources.Vacancies, Icon = "baseline_work_white_24", TargetType = typeof(VacanciesPage), TransparentNavBar = true },
                    new MasterMenuItem { Id = 4, Title = AppResources.Teams, Icon = "baseline_group_white_24", TargetType = typeof(TeamPage), TransparentNavBar = true },
                    new MasterMenuItem { Id = 5, Title = AppResources.Profile, Icon = "baseline_account_box_white_24", TargetType = typeof(PlayerPage), TransparentNavBar = true },
                    new MasterMenuItem { Id = 6, Title = AppResources.Settings, Icon = "baseline_settings_white_24", TargetType = typeof(SettingsPage) },
                    new MasterMenuItem { Id = 7, Title = AppResources.LogIn, Icon = "baseline_settings_white_24", TargetType = typeof(LoginPage), TransparentNavBar = true }
                });
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