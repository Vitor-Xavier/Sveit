using Sveit.Base.ViewModels;
using Sveit.Controls;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Requests;
using Sveit.Services.Team;
using Sveit.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class VacancyViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly IRequestService _requestService;

        private readonly ITeamService _teamService;

        private Vacancy vacancy;

        public Vacancy Vacancy
        {
            get { return vacancy; }
            set { vacancy = value; OnPropertyChanged(); }
        }

        public IAsyncCommand ApplyCommand => new AsyncCommand(ApplyCommandExecute);

        public ObservableCollection<Player> Members { get; set; }

        public VacancyViewModel(INavigation navigation, IRequestService requestService, Vacancy vacancy)
        {
            Vacancy = vacancy;
            _navigation = navigation;
            _requestService = requestService;
            if (AppSettings.ApiStatus)
                _teamService = new TeamService(_requestService);
            else
                _teamService = new FakeTeamService();
        }

        public async Task ApplyCommandExecute()
        {
            if (!await ApplyCommandCanExecute())
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.ApplyFailed);
                return;
            }
            await _navigation.PushModalAsync(new ApplyRegisterPage(_requestService, Vacancy));
        }

        public async Task<bool> ApplyCommandCanExecute()
        {
            if (App.LoggedPlayer == null) return false;

            var members = await _teamService.GetPlayers(Vacancy.TeamId);
            if (members.Any(p => p.PlayerId == App.LoggedPlayer.PlayerId))
                return false;
            return true;
        }
    }
}
