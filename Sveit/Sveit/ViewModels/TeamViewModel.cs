using Sveit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Windows.Input;
using Sveit.Services.Team;
using Sveit.Services.Requests;

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
            LoadData();
            _teamId = 1;
            if (App.IsUsingApi)
                _teamService = new TeamService(new RequestService());
            else
                _teamService = new FakeTeamService();
            Description = TabMembers = true; //TODO: Fix IsVisible update bug
            //DescriptionCommandExecute();
        }

        private void DescriptionCommandExecute()
        {
            TabMembers = false;
            Description = true;
            //LoadProfile();
        }

        private void MembersCommandExecute()
        {
            Description = false;
            TabMembers = true;
            //LoadMembers();
        }

        private void LoadData()
        {
            var gamePlatform = new GamePlatform
            {
                Game = new Game { Name = "Overwatch", ImageSource = "http://fullhdpictures.com/wp-content/uploads/2016/03/Magnificent-Overwatch-Wallpaper.png", IconSource = "http://esportsobserver.com/wp-content/uploads/2015/09/Tt-eSPORTS-logo.png" },
                Platform = new Platform { Name = "PC" }
            };

            Members = new ObservableCollection<Player>
            {
                new Player { Name = "Alguém", Nickname = "Fate", AvatarSource = "https://i.pinimg.com/736x/4a/3f/12/4a3f129ec33ab700ee2f7a022e545cdc--avatar.jpg", Gender = new Gender() { Name = "Masculino" } },
                new Player { Name = "Alguém", Nickname = "envy", AvatarSource = "https://i.pinimg.com/236x/c9/0b/43/c90b43c70af2c51d92814a0b08aff527--the-phantom-avatar.jpg", Gender = new Gender() { Name = "Masculino" } },
                new Player { Name = "Alguém", Nickname = "Verbo", AvatarSource = "https://i.pinimg.com/736x/4a/3f/12/4a3f129ec33ab700ee2f7a022e545cdc--avatar.jpg", Gender = new Gender { Name = "Masculino" } },
                new Player { Name = "Alguém", Nickname = "KariV", AvatarSource = "https://i.pinimg.com/236x/c9/0b/43/c90b43c70af2c51d92814a0b08aff527--the-phantom-avatar.jpg", Gender = new Gender { Name = "Masculino" } },
                new Player { Name = "Alguém", Nickname = "GrimReality", AvatarSource = "https://i.pinimg.com/736x/4a/3f/12/4a3f129ec33ab700ee2f7a022e545cdc--avatar.jpg", Gender = new Gender { Name = "Masculino" } },
                new Player { Name = "Alguém", Nickname = "Agilities", AvatarSource = "https://i.pinimg.com/236x/c9/0b/43/c90b43c70af2c51d92814a0b08aff527--the-phantom-avatar.jpg", Gender = new Gender { Name = "Masculino" } }
            };

            Team = new Team
            {
                Name = "Immortals",
                GamePlatform = gamePlatform,
                IconSource = "http://liquipedia.net/commons/images/thumb/1/1d/Immortals_org.png/600px-Immortals_org.png",
                Owner = new Player { Name = "Owner player", Nickname = "Owner_" }
            };
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
