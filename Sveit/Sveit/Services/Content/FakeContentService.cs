using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sveit.Services.Content
{
    public class FakeContentService : IContentService
    {
        private IEnumerable<Models.Content> Contents { get; set; }

        public FakeContentService()
        {
            Contents = GetFakeContents();
        }

        private IEnumerable<Models.Content> GetFakeContents()
        {
            yield return new Models.Content { ContentId = 1, Title = "Counter-Strike: SK GAMING está entre as participantes da ESL ONE Nova York", Description = "Entre 15 e 17 de setembro, a equipe brasileira de Counter-Strike SK Gaming participará de mais um torneio do game de tiro. A ESL One Nova York distribuirá US$ 250 mil entre os times participantes...", ImageSource = "https://i.ytimg.com/vi/ZLZ3CzBjlPA/maxresdefault.jpg", Source = "IGN", CreatedAt = DateTime.Now };
            yield return new Models.Content { ContentId = 2, Title = "Counter-Strike: SK GAMING está entre as participantes da ESL ONE Nova York", Description = "Entre 15 e 17 de setembro, a equipe brasileira de Counter-Strike SK Gaming participará de mais um torneio do game de tiro. A ESL One Nova York distribuirá US$ 250 mil entre os times participantes...", ImageSource = "https://i.ytimg.com/vi/ZLZ3CzBjlPA/maxresdefault.jpg", Source = "IGN", CreatedAt = DateTime.Today };
            yield return new Models.Content { ContentId = 3, Title = "Counter-Strike: SK GAMING está entre as participantes da ESL ONE Nova York", Description = "Entre 15 e 17 de setembro, a equipe brasileira de Counter-Strike SK Gaming participará de mais um torneio do game de tiro. A ESL One Nova York distribuirá US$ 250 mil entre os times participantes...", ImageSource = "https://i.ytimg.com/vi/ZLZ3CzBjlPA/maxresdefault.jpg", Source = "IGN", CreatedAt = DateTime.Today.AddMonths(-4) };
        }

        public Task<IEnumerable<Models.Content>> GetContentsAsync(DateTime? initialDate = null, DateTime? finalDate = null)
        {
            return Task.FromResult(Contents);
        }

        public Task<bool> PostContentAsync(Models.Content content)
        {
            return Task.FromResult(false);
        }

        public Task<bool> DeleteContentAsync(int contentId)
        {
            return Task.FromResult(false);
        }
    }
}
