using Sveit.Models;
using Sveit.Views;
using Sveit.Services.Comment;
using Sveit.Services.Player;
using Sveit.Services.Requests;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using Sveit.Extensions;
using System.Threading.Tasks;

namespace Sveit.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly IPlayerService _playerService;

        private readonly ICommentService _commentService;

        private readonly int _playerId;

        private bool isCurrentPlayer;

        public bool IsCurrentPlayer
        {
            get { return isCurrentPlayer; }
            set { isCurrentPlayer = value; OnPropertyChanged(); }
        }


        private bool profile;

        public bool Profile
        {
            get { return profile; }
            set { profile = value; OnPropertyChanged(); }
        }

        private bool team;

        public bool Team
        {
            get { return team; }
            set { team = value; OnPropertyChanged(); }
        }

        private bool comment;

        public bool Comment
        {
            get { return comment; }
            set { comment = value; OnPropertyChanged(); }
        }

        private Player player;

        public Player Player
        {
            get { return player; }
            set { player = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Skill> Skills { get; set; }

        public ObservableCollection<Team> Teams { get; set; }

        public ObservableCollection<Comment> Comments { get; set; }

        public IAsyncCommand ProfileCommand => new AsyncCommand(ProfileCommandExecute);

        public IAsyncCommand TeamCommand => new AsyncCommand(TeamCommandExecute);

        public IAsyncCommand CommentCommand => new AsyncCommand(CommentCommandExecute);

        public ICommand AddCommentCommand => new Command(AddCommentCommandExecute);

        public ICommand AddTeamCommand => new Command(AddTeamCommandExecute);

        public ICommand AppliesCommand => new Command(AppliesCommandExecute);

        public ICommand TeamSelectedCommand => new Command<Team>(TeamSelectedCommandExecute);

        public PlayerViewModel(INavigation navigation, IRequestService requestService)
        {
            _navigation = navigation;
            Skills = new ObservableCollection<Skill>();
            Teams = new ObservableCollection<Team>();
            Comments = new ObservableCollection<Comment>();

            if (AppSettings.ApiStatus)
            {
                _playerService = new PlayerService(requestService);
                _commentService = new CommentService(requestService);
            }
            else
            {
                _playerService = new FakePlayerService();
                _commentService = new FakeCommentService();
            }
            _playerId = App.LoggedPlayer.PlayerId;
            Task.Run(() => ProfileCommandExecute());
        }

        private async Task ProfileCommandExecute()
        {
            if (Profile) return;
            Comment = Team = false;
            Profile = true;
            if (Player == null)
                await LoadProfile();
        }

        private async Task TeamCommandExecute()
        {
            if (Team) return;
            Comment = Profile = false;
            Team = true;
            if (Teams?.Count == 0)
                await LoadTeams();
        }

        private async Task CommentCommandExecute()
        {
            if (Comment) return;
            Team = Profile = false;
            Comment = true;
            if (Comments?.Count == 0)
                await LoadComments();
        }

        private async void AddTeamCommandExecute()
        {
            await _navigation.PushModalAsync(new TeamRegisterPage());
        }

        private async void AddCommentCommandExecute()
        {
            await _navigation.PushModalAsync(new CommentRegisterPage());
        }

        private async void AppliesCommandExecute()
        {
            await _navigation.PushAsync(new AppliesPlayerPage(_playerId));
        }

        public async void TeamSelectedCommandExecute(Team team)
        {
            await _navigation.PushAsync(new TeamPage());
        }

        private async Task LoadProfile()
        {
            Player = await _playerService.GetPlayerById(_playerId);

            var skills = await _playerService.GetSkills(_playerId);

            Skills.Clear();
            foreach (Skill skill in skills)
                Skills.Add(skill);
        }

        private async Task LoadTeams()
        {
            var teams = await _playerService.GetPlayerTeams(_playerId);

            Teams.Clear();
            foreach (Team team in teams)
                Teams.Add(team);
        }

        private async Task LoadComments()
        {
            var comments = await _commentService.GetCommentsToPlayer(_playerId);

            Comments.Clear();
            foreach (Comment comment in comments)
                Comments.Add(comment);
        }
    }
}
