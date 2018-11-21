using Sveit.Controls;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Requests;
using Sveit.Services.Team;
using Sveit.Utils;
using Sveit.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class TeamViewModel : BaseViewModel
    {
        public readonly INavigation _navigation;

        private readonly IRequestService _requestService;

        private readonly ITeamService _teamService;

        private readonly int _teamId;

        private bool isOwner;

        public bool IsOwner
        {
            get { return isOwner; }
            set
            {
                isOwner = value;
                OnPropertyChanged();
            }
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

        public IAsyncCommand AddVacancyCommand => new AsyncCommand(AddVacancyCommandExecute);

        public IAsyncCommand DeleteCommand => new AsyncCommand(DeleteCommandExecute);

        public IAsyncCommand<Player> RemovePlayerCommand => new AsyncCommand<Player>(RemovePlayerCommandExecute);

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
            _requestService = requestService;
            _teamId = teamId;
            if (AppSettings.ApiStatus)
                _teamService = new TeamService(requestService);
            else
                _teamService = new FakeTeamService();
            Members = new ObservableCollection<Player>();

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
            await _navigation.PushAsync(new VacanciesTeamPage(_requestService, _teamId));
        }

        private async Task UpdateCommandExecute()
        {
            await _navigation.PushAsync(new TeamRegisterPage(_requestService, Team.TeamId));
        }

        private async Task RemovePlayerCommandExecute(Player player)
        {
            if (!IsOwner) return;
            try
            {
                var result = false; //await _teamService.DeleteTeamPlayerAsync(Team.TeamId, player.PlayerId);
                if (result)
                    await LoadMembers();
                else
                    DependencyService.Get<IMessage>().ShortAlert(AppResources.PlayerLeaveFailed);

            }
            catch
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.PlayerLeaveFailed);
            }
        }

        private async Task DeleteCommandExecute()
        {
            try
            {
                bool result = await _teamService.DeleteTeam(Team.TeamId);
                if (result)
                {
                    App.Current.MainPage = new MasterMainPage(_requestService);
                    return;
                }
                else
                    DependencyService.Get<IMessage>().ShortAlert(AppResources.DeleteTeamFailed);
            }
            catch
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.DeleteTeamFailed);
            }
        }

        private async Task LoadProfile()
        {
            Team = await _teamService.GetById(_teamId);
            IsOwner = App.LoggedPlayer.PlayerId == Team.OwnerId;
        }

        private async Task LoadMembers()
        {
            var members = await _teamService.GetPlayers(_teamId);

            Members.Clear();
            foreach (Player player in members)
                Members.Add(player);
        }

        private async Task AddVacancyCommandExecute()
        {
            await _navigation.PushModalAsync(new Views.VacancyRegisterPage(_requestService, Team));
        }
    }
}
