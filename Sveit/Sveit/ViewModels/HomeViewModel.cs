using Sveit.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sveit.Services.Requests;
using Sveit.Services.Content;

namespace Sveit.ViewModels
{
    public class HomeViewModel
    {
        public ObservableCollection<Content> News { get; set; }

        private readonly IContentService _contentService;

        public HomeViewModel()
        {
            _contentService = new ContentService(new RequestService());
            News = new ObservableCollection<Content>();

            LoadData(AppSettings.ApiStatus);
        }

        private async void LoadData(bool api)
        {
            if (!api)
            {
                News.Clear();
                News.Add(new Content { ContentId = 1, Title = "Counter-Strike: SK GAMING está entre as participantes da ESL ONE Nova York", Description = "Entre 15 e 17 de setembro, a equipe brasileira de Counter-Strike SK Gaming participará de mais um torneio do game de tiro. A ESL One Nova York distribuirá US$ 250 mil entre os times participantes...", ImageSource = "http://www.nerdweek.com.br/wp-content/uploads/2017/05/image006.jpg", Source = "IGN", CreatedAt = DateTime.Now });
                News.Add(new Content { ContentId = 2, Title = "Counter-Strike: SK GAMING está entre as participantes da ESL ONE Nova York", Description = "Entre 15 e 17 de setembro, a equipe brasileira de Counter-Strike SK Gaming participará de mais um torneio do game de tiro. A ESL One Nova York distribuirá US$ 250 mil entre os times participantes...", ImageSource = "http://www.nerdweek.com.br/wp-content/uploads/2017/05/image006.jpg", Source = "IGN", CreatedAt = DateTime.Today });
                News.Add(new Content { ContentId = 3, Title = "Counter-Strike: SK GAMING está entre as participantes da ESL ONE Nova York", Description = "Entre 15 e 17 de setembro, a equipe brasileira de Counter-Strike SK Gaming participará de mais um torneio do game de tiro. A ESL One Nova York distribuirá US$ 250 mil entre os times participantes...", ImageSource = "http://www.nerdweek.com.br/wp-content/uploads/2017/05/image006.jpg", Source = "IGN", CreatedAt = DateTime.Today.AddMonths(-4) });
            } else
            {
                var contents = await _contentService.GetContentsAsync();

                News.Clear();
                foreach (Content content in contents)
                {
                    News.Add(content);
                }
            }
            
        }
    }
}
