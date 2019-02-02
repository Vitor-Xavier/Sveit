using Sveit.Base.ViewModels;
using Sveit.Controls;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Comment;
using Sveit.Services.Requests;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class CommentRegisterViewModel : BaseViewModel
    {
        private readonly int _commentId;

        private readonly INavigation _navigation;

        private readonly ICommentService _commentService;

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged(); }
        }

        private Player playerTo;

        public Player PlayerTo
        {
            get { return playerTo; }
            set { playerTo = value; OnPropertyChanged(); }
        }

        public IAsyncCommand FinalizeCommand => new AsyncCommand(FinalizeCommandExecute);

        public CommentRegisterViewModel(INavigation navigation, IRequestService requestService, Player player, int commentId = 0)
        {
            _commentId = commentId;
            _navigation = navigation;
            if (AppSettings.ApiStatus)
                _commentService = new CommentService(requestService);
            else
                _commentService = new FakeCommentService();
            PlayerTo = player;

            Task.Run(() => LoadComment());
        }

        private async Task FinalizeCommandExecute()
        {
            if (!FinalizeCommandCanExecute())
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.CommentFailed);
                return;
            }
            var comment = new Comment
            {
                CommentId = _commentId,
                ToPlayerId = PlayerTo.PlayerId,
                FromPlayerId = App.LoggedPlayer.PlayerId,
                Text = Text
            };

            var registered = await _commentService.PostComment(comment);
            if (registered != null)
                await _navigation.PopModalAsync();
            else
                DependencyService.Get<IMessage>().ShortAlert(AppResources.CommentFailed);
        }

        private bool FinalizeCommandCanExecute()
        {
            if (string.IsNullOrWhiteSpace(Text) && Text.Length <= 60)
                return false;
            return true;
        }

        private async Task LoadComment()
        {
            if (_commentId != 0) return;

            try
            {
                var comment = await _commentService.GetById(_commentId);
                if (comment == null) return;
                Text = comment.Text;
            }
            catch
            {

            }
        }
    }
}
