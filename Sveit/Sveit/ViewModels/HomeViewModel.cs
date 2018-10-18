using Sveit.Models;
using Sveit.Services.Content;
using Sveit.Services.Requests;
using System.Collections.ObjectModel;

namespace Sveit.ViewModels
{
    public class HomeViewModel
    {
        public ObservableCollection<Content> News { get; set; }

        private readonly IContentService _contentService;

        public HomeViewModel()
        {
            if (AppSettings.ApiStatus)
                _contentService = new ContentService(new RequestService());
            else
                _contentService = new FakeContentService();
            News = new ObservableCollection<Content>();

            LoadData();
        }

        private async void LoadData()
        {
            var contents = await _contentService.GetContentsAsync();

            News.Clear();
            foreach (Content content in contents)
                News.Add(content);
        }
    }
}
