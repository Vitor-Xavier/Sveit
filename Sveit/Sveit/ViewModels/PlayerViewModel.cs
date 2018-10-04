using Sveit.Models;
using Sveit.Views;
using Sveit.Services.Comment;
using Sveit.Services.Player;
using Sveit.Services.Requests;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System;

namespace Sveit.ViewModels
{
    public class PlayerViewModel : BindableObject
    {
        private readonly INavigation _navigation;

        private readonly IPlayerService _playerService;

        private readonly ICommentService _commentService;

        private readonly int _playerId;

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

        public ICommand ProfileCommand => new Command(ProfileCommandExecute);

        public ICommand TeamCommand => new Command(TeamCommandExecute);

        public ICommand CommentCommand => new Command(CommentCommandExecute);

        public ICommand AddCommentCommand => new Command(AddCommentCommandExecute);

        public ICommand AddTeamCommand => new Command(AddTeamCommandExecute);

        public PlayerViewModel(INavigation navigation)
        {
            _navigation = navigation;
            IRequestService requestService = new RequestService();
            Skills = new ObservableCollection<Skill>();
            Teams = new ObservableCollection<Team>();
            Comments = new ObservableCollection<Comment>();

            if (App.IsUsingApi)
            {
                _playerService = new PlayerService(requestService);
                _commentService = new CommentService(requestService);
            }
            else
            {
                _playerService = new FakePlayerService();
                _commentService = new FakeCommentService();
            }
            _playerId = 1;
            LoadProfile(); LoadTeams(); LoadComments();
            ProfileCommandExecute();
            Comment = Team = Profile = true; //TODO: Fix IsVisible update bug
            //ProfileCommandExecute();
        }

        private void ProfileCommandExecute()
        {
            //if (Profile) return;
            Comment = Team = false;
            Profile = true;
            if (Player == null)
                LoadProfile();
        }

        private void TeamCommandExecute()
        {
            //if (Team) return;
            Comment = Profile = false;
            Team = true;
            if (Teams?.Count == 0)
                LoadTeams();
        }

        private void CommentCommandExecute()
        {
            //if (Comment) return;
            Team = Profile = false;
            Comment = true;
            if (Comments?.Count == 0)
                LoadComments();
        }

        private async void AddTeamCommandExecute()
        {
            await _navigation.PushModalAsync(new TeamRegisterPage());
        }

        private async void AddCommentCommandExecute()
        {
            await _navigation.PushModalAsync(new CommentRegisterPage());
        }

        private async void LoadProfile()
        {
            Player = await _playerService.GetPlayerById(_playerId);
            var skills = await _playerService.GetSkills(_playerId);

            Skills.Clear();
            foreach (Skill skill in skills)
                Skills.Add(skill);
        }

        private async void LoadTeams()
        {
            var teams = await _playerService.GetPlayerTeams(_playerId);

            Teams.Clear();
            foreach (Team team in teams)
                Teams.Add(team);
        }

        private async void LoadComments()
        {
            var comments = await _commentService.GetCommentsToPlayer(_playerId);

            Comments.Clear();
            foreach (Comment comment in comments)
                Comments.Add(comment);
        }
    }
}
