using Sveit.Base.ViewModels;
using Sveit.Controls;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Team;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class VacancyViewModel : BaseViewModel
    {
        private readonly ITeamService _teamService;

        private Vacancy vacancy;

        public Vacancy Vacancy
        {
            get { return vacancy; }
            set { vacancy = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Player> Members { get; set; }

        public IAsyncCommand ApplyCommand => new AsyncCommand(ApplyCommandExecute);

        public VacancyViewModel(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public override Task InitializeAsync(params object[] navigationData)
        {
            Vacancy = navigationData[0] as Vacancy;
            return base.InitializeAsync(navigationData);
        }

        public async Task ApplyCommandExecute()
        {
            if (!await ApplyCommandCanExecute())
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.ApplyFailed);
                return;
            }
            await NavigationService.NavigateToAsync<ApplyRegisterViewModel>(Vacancy);
            //await _navigation.PushModalAsync(new ApplyRegisterPage(_requestService, Vacancy));
        }

        public async Task<bool> ApplyCommandCanExecute()
        {
            if (App.LoggedPlayer == null) return false;

            var members = await _teamService.GetPlayers(Vacancy.TeamId);
            return members.Any(p => p.PlayerId == App.LoggedPlayer.PlayerId);
        }
    }
}
