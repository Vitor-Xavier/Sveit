using Sveit.Models;
using Sveit.Services.Login;
using Sveit.Services.Requests;
using Sveit.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.ViewModels
{
    public class MasterMainViewModel : BaseViewModel
    {
        private ILoginService _loginService;

        public ObservableCollection<Views.MasterMenuItem> MenuItems { get; set; }

        private Player loggedPlayer;

        public Player LoggedPlayer
        {
            get { return loggedPlayer; }
            set { loggedPlayer = value; OnPropertyChanged(); IsLogged = value != null; }
        }

        private bool isLogged;

        public bool IsLogged
        {
            get { return isLogged; }
            set { isLogged = value; OnPropertyChanged(); }
        }

        public MasterMainViewModel(IRequestService requestService)
        {
            if (AppSettings.ApiStatus)
                _loginService = new LoginService(requestService);
            else
                _loginService = new FakeLoginService();
            MenuItems = new ObservableCollection<Views.MasterMenuItem>();
            Task.Run(() => LoadItems());
        }

        private async Task LoadItems()
        {
            LoggedPlayer = await _loginService.CheckLogIn();

            if (LoggedPlayer == null)
            {
                MenuItems.Add(new Views.MasterMenuItem { Id = 0, Title = AppResources.LogIn, Icon = "baseline_settings_white_24", TargetType = typeof(LoginPage), TransparentNavBar = true });
                MenuItems.Add(new Views.MasterMenuItem { Id = 1, Title = AppResources.News, Icon = "baseline_fiber_new_white_24", TargetType = typeof(HomePage) });
                MenuItems.Add(new Views.MasterMenuItem { Id = 3, Title = AppResources.Games, Icon = "baseline_gamepad_white_24", TargetType = typeof(GamesPage), TransparentNavBar = true });
                MenuItems.Add(new Views.MasterMenuItem { Id = 4, Title = AppResources.Vacancies, Icon = "baseline_work_white_24", TargetType = typeof(VacanciesPage), TransparentNavBar = true });
                MenuItems.Add(new Views.MasterMenuItem { Id = 5, Title = AppResources.Settings, Icon = "baseline_settings_white_24", TargetType = typeof(SettingsPage) });
            }
            else
            {
                MenuItems.Add(new Views.MasterMenuItem { Id = 1, Title = AppResources.News, Icon = "baseline_fiber_new_white_24", TargetType = typeof(HomePage) });
                MenuItems.Add(new Views.MasterMenuItem { Id = 2, Title = AppResources.Profile, Icon = "baseline_account_box_white_24", TargetType = typeof(PlayerPage), TransparentNavBar = true });
                MenuItems.Add(new Views.MasterMenuItem { Id = 3, Title = AppResources.Games, Icon = "baseline_gamepad_white_24", TargetType = typeof(GamesPage), TransparentNavBar = true });
                MenuItems.Add(new Views.MasterMenuItem { Id = 4, Title = AppResources.Vacancies, Icon = "baseline_work_white_24", TargetType = typeof(VacanciesPage), TransparentNavBar = true });
                MenuItems.Add(new Views.MasterMenuItem { Id = 5, Title = AppResources.Settings, Icon = "baseline_settings_white_24", TargetType = typeof(SettingsPage) });
                MenuItems.Add(new Views.MasterMenuItem { Id = 6, Title = AppResources.Exit, Icon = "baseline_settings_white_24" });
            }
        }
    }
}
