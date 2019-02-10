﻿using Sveit.Base.ViewModels;
using Sveit.Controls;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Comment;
using Sveit.Services.Login;
using Sveit.Services.Player;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class PlayerViewModel : BaseViewModel
    {
        private readonly IPlayerService _playerService;

        private readonly ICommentService _commentService;

        private int _playerId;

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

        public IAsyncCommand TeamsCommand => new AsyncCommand(TeamsCommandExecute);

        public IAsyncCommand CommentsCommand => new AsyncCommand(CommentsCommandExecute);

        public IAsyncCommand ContactsCommand => new AsyncCommand(ContactsCommandExecute);

        public IAsyncCommand AddCommentCommand => new AsyncCommand(AddCommentCommandExecute);

        public IAsyncCommand AddTeamCommand => new AsyncCommand(AddTeamCommandExecute);

        public IAsyncCommand AppliesCommand => new AsyncCommand(AppliesCommandExecute);

        public IAsyncCommand DeleteCommand => new AsyncCommand(DeleteCommandExecute);

        public IAsyncCommand UpdateCommand => new AsyncCommand(UpdateCommandExecute);

        public IAsyncCommand<Team> TeamCommand => new AsyncCommand<Team>(TeamCommandExecute);

        public IAsyncCommand<Team> LeaveTeamCommand => new AsyncCommand<Team>(LeaveTeamCommandExecute);

        public IAsyncCommand<Comment> UpdateCommentCommand => new AsyncCommand<Comment>(UpdateCommentCommandExecute);

        public PlayerViewModel(IPlayerService playerService, ICommentService commentService)
        {
            _commentService = commentService;
            _playerService = playerService;

            Skills = new ObservableCollection<Skill>();
            Teams = new ObservableCollection<Team>();
            Comments = new ObservableCollection<Comment>();
        }

        public override Task InitializeAsync(params object[] navigationData)
        {
            _playerId = navigationData[0] as int? ?? App.LoggedPlayer.PlayerId;
            IsCurrentPlayer = _playerId == App.LoggedPlayer.PlayerId;
            return ProfileCommandExecute();
        }

        private async Task ProfileCommandExecute()
        {
            if (Profile) return;
            Comment = Team = false;
            Profile = true;
            if (Player == null)
                await LoadProfile();
        }

        private async Task TeamsCommandExecute()
        {
            if (Team) return;
            Comment = Profile = false;
            Team = true;
            if (Teams?.Count == 0)
                await LoadTeams();
        }

        private async Task CommentsCommandExecute()
        {
            if (Comment) return;
            Team = Profile = false;
            Comment = true;
            if (Comments?.Count == 0)
                await LoadComments();
        }

        private async Task DeleteCommandExecute()
        {
            try
            {
                bool result = await _playerService.DeletePlayerAsync(Player.PlayerId);
                if (result)
                {
                    var loginService = Base.Locator.Instance.Resolve<ILoginService>();
                    loginService.LogOut();
                    await NavigationService.NavigateToAsync<MasterDetailMainViewModel>();
                    //App.Current.MainPage = new MasterDetailMainPage(_requestService);
                }
                else
                    DependencyService.Get<IMessage>().ShortAlert(AppResources.DeleteProfileFailed);
            }
            catch
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.DeleteProfileFailed);
            }
        }

        private async Task AddTeamCommandExecute()
        {
            await NavigationService.NavigateToAsync<TeamRegisterViewModel>();
            //await _navigation.PushModalAsync(new TeamRegisterPage(_requestService));
        }

        private async Task AddCommentCommandExecute()
        {
            await NavigationService.NavigateToAsync<CommentRegisterViewModel>(Player);
            //await _navigation.PushModalAsync(new CommentRegisterPage(_requestService, Player));
        }

        private async Task AppliesCommandExecute()
        {
            await NavigationService.NavigateToAsync<AppliesPlayerViewModel>(_playerId);
        }

        public async Task TeamCommandExecute(Team team)
        {
            bool isOwner = Player.PlayerId == team.OwnerId;
            await NavigationService.NavigateToAsync<TeamViewModel>(team.TeamId, isOwner);
            //TODO: t
            //await _navigation.PushAsync(new TeamPage(_requestService, team.TeamId, isOwner));
        }

        private async Task UpdateCommentCommandExecute(Comment comment)
        {
            if (comment == null) return;
            if (comment.FromPlayerId == App.LoggedPlayer?.PlayerId)
            {
                await NavigationService.NavigateToAsync<CommentRegisterViewModel>();
                //TODO: t
                //await _navigation.PushModalAsync(new CommentRegisterPage(_requestService, Player, comment.CommentId));
            }
        }

        private async Task ContactsCommandExecute()
        {
            await NavigationService.NavigateToAsync<ContactsPlayerRegisterViewModel>(Player);
            //await _navigation.PushModalAsync(new ContactsPlayerRegisterPage(_requestService, Player));
        }

        private async Task UpdateCommandExecute()
        {
            await NavigationService.NavigateToAsync<PlayerRegisterViewModel>(_playerId);
            //await _navigation.PushAsync(new PlayerRegisterPage(_requestService, Player.PlayerId));
        }

        private async Task LeaveTeamCommandExecute(Team team)
        {
            if (!IsCurrentPlayer) return;
            if (Player.PlayerId == team.OwnerId) return;
            try
            {
                var result = await _playerService.DeleteTeamPlayer(Player.PlayerId, team.TeamId);
                if (result)
                    await LoadTeams();
                else
                    DependencyService.Get<IMessage>().ShortAlert(AppResources.TeamLeaveFailed);

            }
            catch
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.TeamLeaveFailed);
            }
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
