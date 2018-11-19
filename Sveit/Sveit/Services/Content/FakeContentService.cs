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
            yield return new Models.Content
            {
                ContentId = 1,
                Title = "Conheça os sete jogos mais rentáveis do mundo dos esports",
                Description = "Os torneios de esports estão cada vez mais populares no Brasil e no mundo. Além do números de pro players e espectadores ter aumentado, os prêmios dos campeonatos são cada vez mais altos, alcançado cifras milionárias.",
                Source = "techtudo",
                ImageSource = "https://s2.glbimg.com/ZzAugwh6y10N64qSR5YFYZ3pslQ=/0x0:1000x563/984x0/smart/filters:strip_icc()/i.s3.glbimg.com/v1/AUTH_08fbf48bc0524877943fe86e43087e7a/internal_photos/bs/2018/f/X/JrkfRQQrKeGepQdpA4cg/hearthstone1.jpg",
                ContentUrl = "https://www.techtudo.com.br/listas/2018/11/conheca-os-sete-jogos-mais-rentaveis-do-mundo-dos-esports.ghtml",
                CreatedAt = DateTime.Today.AddHours(-5)
            };
            yield return new Models.Content
            {
                ContentId = 2,
                Title = "Athenas é a primeira organização de eSports para mulheres no Brasil",
                Description = "A Athenas e-Sports, primeira organização de eSports brasileira inteiramente dedicada a jogadoras, anunciou sua primeira seletiva de League of Legends.",
                Source = "TheEnemy",
                ImageSource = "https://cdn.ome.lt/5DcRWu2Ih2wo3eceRYkINtZynz8=/970x360/smart/uploads/conteudo/fotos/athenas.jpg",
                ContentUrl = "https://www.theenemy.com.br/esports/athenas-e-a-primeira-organizacao-de-esports-para-mulheres-no-brasil",
                CreatedAt = DateTime.Today.AddDays(-5)
            };
            yield return new Models.Content
            {
                ContentId = 3,
                Title = "Counter-Strike: SK GAMING está entre as participantes da ESL ONE Nova York",
                Description = "Entre 15 e 17 de setembro, a equipe brasileira de Counter-Strike SK Gaming participará de mais um torneio do game de tiro. A ESL One Nova York distribuirá US$ 250 mil entre os times participantes...",
                ImageSource = "https://i.ytimg.com/vi/ZLZ3CzBjlPA/maxresdefault.jpg",
                Source = "IGN",
                CreatedAt = DateTime.Today.AddMonths(-2)
            };
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
