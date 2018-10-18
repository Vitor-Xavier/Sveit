using Sveit.Models;
using Sveit.Services.Requests;
using Sveit.Services.Team;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class TeamViewModel : BindableObject
    {
        private readonly ITeamService _teamService;

        private readonly int _teamId;

        private Team team;

        public Team Team
        {
            get { return team; }
            set { team = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Player> Members { get; set; }

        public ICommand DescriptionCommand => new Command(DescriptionCommandExecute);

        public ICommand MembersCommand => new Command(MembersCommandExecute);

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

        public TeamViewModel()
        {
            _teamId = 1;
            if (AppSettings.ApiStatus)
                _teamService = new TeamService(new RequestService());
            else
                _teamService = new FakeTeamService();
            Members = new ObservableCollection<Player>();
            Description = TabMembers = true; //TODO: Fix IsVisible update bug
            //DescriptionCommandExecute();
        }

        private void DescriptionCommandExecute()
        {
            TabMembers = false;
            Description = true;
            LoadProfile();
        }

        private void MembersCommandExecute()
        {
            Description = false;
            TabMembers = true;
            LoadMembers();
        }

        private async void LoadProfile()
        {
            Team = await _teamService.GetById(_teamId);
        }

        private async void LoadMembers()
        {
            var members = await _teamService.GetPlayers(_teamId);

            Members.Clear();
            foreach (Player player in members)
                Members.Add(player);
        }
    }
}
