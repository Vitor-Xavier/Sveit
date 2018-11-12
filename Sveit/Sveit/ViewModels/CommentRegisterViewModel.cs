using Sveit.Models;
using Sveit.Services.Comment;
using Sveit.Services.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class CommentRegisterViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly ICommentService _commentService;

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged(); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }

        private Player playerTo;

        public Player PlayerTo
        {
            get { return playerTo; }
            set { playerTo = value; OnPropertyChanged(); }
        }

        public ICommand ContinueCommand => new Command(ContinueCommandExecute);

        public CommentRegisterViewModel(INavigation navigation, IRequestService requestService, Player player)
        {
            _navigation = navigation;
            _commentService = new CommentService(requestService);
            PlayerTo = player;
        }

        private async void ContinueCommandExecute()
        {
            var comment = new Comment
            {
                ToPlayerId = PlayerTo.PlayerId,
                FromPlayerId = App.LoggedPlayer.PlayerId,
                Text = Text
            };

            bool registered = await _commentService.PostComment(comment);
            if (registered)
                await _navigation.PopModalAsync();
        }
    }
}
