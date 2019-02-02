using Sveit.Base.ViewModels;
using Sveit.Controls;
using Sveit.Services.Login;
using Sveit.ViewModels;
using Sveit.ViewModels.Base;
using Sveit.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sveit.Services.Navigation
{
    public partial class NavigationService : INavigationService
    {
        readonly ILoginService loginService;
        protected readonly Dictionary<Type, Type> mappings;

        protected Application CurrentApplication => Application.Current;

        public NavigationService(ILoginService loginService)
        {
            this.loginService = loginService;
            mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
        }

        public async Task InitializeAsync()
        {
            //if (await loginService.UserIsAuthenticatedAndValidAsync())
            //{
                await NavigateToAsync<MasterDetailMainViewModel>();
            //}
            //else
            //{
            //    await NavigateToAsync<LoginViewModel>();
            //}
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel => InternalNavigateToAsync(typeof(TViewModel), null);

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel => InternalNavigateToAsync(typeof(TViewModel), parameter);

        public Task NavigateToAsync(Type viewModelType) => InternalNavigateToAsync(viewModelType, null);

        public Task NavigateToAsync(Type viewModelType, object parameter) => InternalNavigateToAsync(viewModelType, parameter);

        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage is MasterDetailMainPage)
            {
                var mainPage = CurrentApplication.MainPage as MasterDetailMainPage;
                await mainPage.Detail.Navigation.PopAsync();
            }
            else if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public virtual Task RemoveLastFromBackStackAsync()
        {
            if (CurrentApplication.MainPage is MasterDetailMainPage mainPage)
            {
                mainPage.Detail.Navigation.RemovePage(
                    mainPage.Detail.Navigation.NavigationStack[mainPage.Detail.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            var page = CreateAndBindPage(viewModelType, parameter);

            if (page is MasterDetailMainPage)
            {
                CurrentApplication.MainPage = page;
            }
            else if (page is LoginPage)
            {
                CurrentApplication.MainPage = new CustomNavigationPage(page);
            }
            else if (CurrentApplication.MainPage is MasterDetailMainPage)
            {
                var mainPage = CurrentApplication.MainPage as MasterDetailMainPage;

                if (mainPage.Detail is CustomNavigationPage navigationPage)
                {
                    var currentPage = navigationPage.CurrentPage;

                    if (currentPage.GetType() != page.GetType())
                    {
                        await navigationPage.PushAsync(page);
                    }
                }
                else
                {
                        navigationPage = new CustomNavigationPage(page);
                        mainPage.Detail = navigationPage; 
                }

                mainPage.IsPresented = false;
            }
            else
            {
                if (CurrentApplication.MainPage is CustomNavigationPage navigationPage)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    CurrentApplication.MainPage = new CustomNavigationPage(page);
                }
            }

            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return mappings[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            var pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            var page = Activator.CreateInstance(pageType) as Page;
            var viewModel = Locator.Instance.Resolve(viewModelType) as BaseViewModel;
            //var viewModel = Locator.Instance.Resolve(viewModelType, new Autofac.NamedParameter("", parameter)) as BaseViewModel;
            page.BindingContext = viewModel;

            return page;
        }

        void CreatePageViewModelMappings()
        {
            mappings.Add(typeof(AppliesPlayerViewModel), typeof(AppliesPlayerPage));
            mappings.Add(typeof(AppliesTeamViewModel), typeof(AppliesTeamPage));
            mappings.Add(typeof(ApplyViewModel), typeof(ApplyPage));
            mappings.Add(typeof(ApplyRegisterViewModel), typeof(ApplyRegisterPage));
            mappings.Add(typeof(CommentRegisterViewModel), typeof(CommentRegisterPage));
            mappings.Add(typeof(ContactsPlayerRegisterViewModel), typeof(ContactsPlayerRegisterPage));
            mappings.Add(typeof(ContactsTeamRegisterViewModel), typeof(ContactsTeamRegisterPage));
            mappings.Add(typeof(GameViewModel), typeof(GamePage));
            mappings.Add(typeof(GamesViewModel), typeof(GamesPage));
            mappings.Add(typeof(HomeViewModel), typeof(HomePage));
            mappings.Add(typeof(LoginViewModel), typeof(LoginPage));
            mappings.Add(typeof(MasterDetailMainViewModel), typeof(MasterDetailMainPage));
            mappings.Add(typeof(MasterMainViewModel), typeof(MasterMainPageMaster));
            mappings.Add(typeof(PlayerViewModel), typeof(PlayerPage));
            mappings.Add(typeof(PlayerRegisterViewModel), typeof(PlayerRegisterPage));
            mappings.Add(typeof(SettingsViewModel), typeof(SettingsPage));
            mappings.Add(typeof(TeamViewModel), typeof(TeamPage));
            mappings.Add(typeof(TeamRegisterViewModel), typeof(TeamRegisterPage));
            mappings.Add(typeof(VacanciesViewModel), typeof(VacanciesPage));
            mappings.Add(typeof(VacanciesTeamViewModel), typeof(VacanciesTeamPage));
            mappings.Add(typeof(VacancyViewModel), typeof(VacancyPage));
            mappings.Add(typeof(VacancyRegisterViewModel), typeof(VacancyRegisterPage));
        }
    }
}
