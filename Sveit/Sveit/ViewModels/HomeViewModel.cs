using Sveit.Base.ViewModels;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Content;
using Sveit.Services.Requests;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IContentService _contentService;

        public ObservableCollection<Content> News { get; set; }

        public ICommand ContentCommand => new Command<Content>(ContentCommandExecute);

        public IAsyncCommand RefreshCommand => new AsyncCommand(RefreshCommandExecute);

        public HomeViewModel(IContentService contentService)
        {
            _contentService = contentService;

            News = new ObservableCollection<Content>();
            
            Task.Run(() => RefreshCommand.ExecuteAsync());
        }

        private async Task RefreshCommandExecute()
        {
            await LoadContent();
        }

        private async Task LoadContent(int count = 0)
        {
            if (count > 1) return;
            if (IsLoading) return;
            IsLoading = true;
            try
            {
                var contents = await _contentService.GetContentsAsync();

                Device.BeginInvokeOnMainThread(() =>
                {
                    News.Clear();
                    foreach (Content content in contents)
                        News.Add(content);
                });
                
            }
            catch
            {
                await LoadContent(count + 1);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ContentCommandExecute(Content content)
        {
            if (content == null) return;
            Device.OpenUri(new Uri(content.ContentUrl));
        }
    }
}
