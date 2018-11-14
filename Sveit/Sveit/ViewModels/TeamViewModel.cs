using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Requests;
using Sveit.Services.Team;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class TeamViewModel : BaseViewModel
    {
        public readonly INavigation _navigation;

        private readonly ITeamService _teamService;

        private readonly int _teamId;

        private bool isOwner;

        public bool IsOwner
        {
            get { return isOwner; }
            set { isOwner = value; OnPropertyChanged(); }
        }

        private Team team;

        public Team Team
        {
            get { return team; }
            set { team = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Player> Members { get; set; }

        public IAsyncCommand DescriptionCommand => new AsyncCommand(DescriptionCommandExecute);

        public IAsyncCommand MembersCommand => new AsyncCommand(MembersCommandExecute);

        public IAsyncCommand VacanciesCommand => new AsyncCommand(VacanciesCommandExecute);

        private bool tabMembers;

        public bool TabMembers
        {
            get { return tabMembers; }
            set
            {
                tabMembers = value;
                OnPropertyChanged();
            }
        }

        private bool description;

        public bool Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        public TeamViewModel(INavigation navigation, IRequestService requestService, int teamId)
        {
            _navigation = navigation;
            _teamId = teamId;
            if (AppSettings.ApiStatus)
                _teamService = new TeamService(requestService);
            else
                _teamService = new FakeTeamService();
            Members = new ObservableCollection<Player>();
            //TODO: Restritions
            //IsOwner = App.LoggedPlayer.PlayerId == Team.

            Task.Run(() => DescriptionCommandExecute());
        }

        private async Task DescriptionCommandExecute()
        {
            TabMembers = false;
            Description = true;
            await LoadProfile();
        }

        private async Task MembersCommandExecute()
        {
            Description = false;
            TabMembers = true;
            await LoadMembers();
        }

        private async Task VacanciesCommandExecute()
        {
            await _navigation.PushAsync(new Views.VacanciesTeamPage(_teamId));
        }

        private async Task LoadProfile()
        {
            Team = await _teamService.GetById(_teamId);
        }

        private async Task LoadMembers()
        {
            var members = await _teamService.GetPlayers(_teamId);

            Members.Clear();
            foreach (Player player in members)
                Members.Add(player);
        }
    }
}
