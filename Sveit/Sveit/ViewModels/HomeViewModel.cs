using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Content;
using Sveit.Services.Requests;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<Content> News { get; set; }

        private readonly IContentService _contentService;

        public IAsyncCommand RefreshCommand => new AsyncCommand(RefreshCommandExecute);

        public HomeViewModel(IRequestService requestService)
        {
            if (AppSettings.ApiStatus)
                _contentService = new ContentService(requestService);
            else
                _contentService = new FakeContentService();
            News = new ObservableCollection<Content>();

            Task.Run(() => RefreshCommandExecute()).ConfigureAwait(false);
        }

        private async Task RefreshCommandExecute()
        {
            if (IsLoading) return;
            IsLoading = true;
            var contents = await _contentService.GetContentsAsync();

            News.Clear();
            foreach (Content content in contents)
                News.Add(content);

            IsLoading = false;
        }
    }
}
