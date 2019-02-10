using Sveit.Base.ViewModels;
using Sveit.Controls;
using Sveit.Models;
using Sveit.Services.Login;
using Sveit.Services.Requests;
using Sveit.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class MasterMainViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;

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

        public MasterMainViewModel(ILoginService loginService)
        {
            _loginService = loginService;
            MenuItems = new ObservableCollection<Views.MasterMenuItem>();
            Task.Run(() => LoadItems());
        }

        private async Task LoadItems()
        {
            try
            {
                LoggedPlayer = await _loginService.CheckLogIn();
            }
            catch
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.LoginFailed);
            }
            finally
            {
                if (LoggedPlayer == null)
                {
                    MenuItems.Add(new Views.MasterMenuItem { Id = 0, Title = AppResources.LogIn, Icon = "baseline_settings_white_24", TargetType = typeof(LoginViewModel), TransparentNavBar = true });
                    MenuItems.Add(new Views.MasterMenuItem { Id = 1, Title = AppResources.News, Icon = "baseline_fiber_new_white_24", TargetType = typeof(HomeViewModel) });
                    MenuItems.Add(new Views.MasterMenuItem { Id = 3, Title = AppResources.Games, Icon = "baseline_gamepad_white_24", TargetType = typeof(GamesViewModel), TransparentNavBar = true });
                    MenuItems.Add(new Views.MasterMenuItem { Id = 4, Title = AppResources.Vacancies, Icon = "baseline_work_white_24", TargetType = typeof(VacanciesViewModel), TransparentNavBar = true });
                    MenuItems.Add(new Views.MasterMenuItem { Id = 5, Title = AppResources.Settings, Icon = "baseline_settings_white_24", TargetType = typeof(SettingsViewModel) });
                }
                else
                {
                    MenuItems.Add(new Views.MasterMenuItem { Id = 1, Title = AppResources.News, Icon = "baseline_fiber_new_white_24", TargetType = typeof(HomeViewModel) });
                    MenuItems.Add(new Views.MasterMenuItem { Id = 2, Title = AppResources.Profile, Icon = "baseline_account_box_white_24", TargetType = typeof(PlayerViewModel), TransparentNavBar = true });
                    MenuItems.Add(new Views.MasterMenuItem { Id = 3, Title = AppResources.Games, Icon = "baseline_gamepad_white_24", TargetType = typeof(GamesViewModel), TransparentNavBar = true });
                    MenuItems.Add(new Views.MasterMenuItem { Id = 4, Title = AppResources.Vacancies, Icon = "baseline_work_white_24", TargetType = typeof(VacanciesViewModel), TransparentNavBar = true });
                    MenuItems.Add(new Views.MasterMenuItem { Id = 5, Title = AppResources.Settings, Icon = "baseline_settings_white_24", TargetType = typeof(SettingsViewModel) });
                    MenuItems.Add(new Views.MasterMenuItem { Id = 6, Title = AppResources.Exit, Icon = "exit_to_app_white" });
                }
            }
        }

        public async Task ItemSelected(MasterMenuItem menuItem)
        {
            if (menuItem.Id == 6)
            {
                if (_loginService.LogOut())
                    await NavigationService.NavigateToAsync<MasterDetailMainViewModel>();
                return;
            }
            await NavigationService.NavigateToAsync(menuItem.TargetType, menuItem);
        }
    }
}
