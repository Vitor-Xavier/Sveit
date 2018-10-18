using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sveit.Models;

namespace Sveit.Services.Game
{
    public class FakeGameService : IGameService
    {
        public IEnumerable<Models.Game> Games { get; private set; }

        public FakeGameService()
        {
            Games = GetFakeGames();
        }

        private IEnumerable<Models.Game> GetFakeGames()
        {
            Models.Genre genreFPS = new Models.Genre() { GenreId = 1, Name = "FPS" };
            Models.Genre genreAction = new Models.Genre() { GenreId = 1, Name = "Action" };
            Models.Genre genreSurvival = new Models.Genre() { GenreId = 5, Name = "Survival" };
            Models.Genre genreSurvivalHorror = new Models.Genre { GenreId = 6, Name = "Survival Horror" };
            yield return new Models.Game
            {
                GameId = 1,
                Name = "Rainbow Six Siege",
                Description = "FPS mapas, estratégia...",
                Genres = new List<Models.Genre> { genreFPS },
                IconSource = "https://i.redd.it/iznunq2m8vgy.png",
                ImageSource = "https://www.torcedores.com/content/uploads/2018/02/erwmqs3hzvkfqudfutqdyh_tb7c.jpg"
            };
            yield return new Models.Game
            {
                GameId = 2,
                Name = "Overwatch",
                Description = "FPS, objetivo, 5 membros",
                Genres = new List<Models.Genre> { genreFPS },
                IconSource = "https://i.imgur.com/0RIw2RB.png",
                ImageSource = "http://www.base2.com.br/wp-content/uploads/2017/04/overwatch1280jpg-6daa73_1280w.jpg"
            };
            yield return new Models.Game
            {
                GameId = 3,
                Name = "Destiny 2",
                Description = "FPS ",
                Genres = new List<Models.Genre> { genreFPS },
                IconSource = "http://i44.servimg.com/u/f44/13/94/19/07/destin10.png",
                ImageSource = "https://www.pcguia.pt/wp-content/uploads/2018/02/Destiny-2-900x445.jpg"
            };
            yield return new Models.Game
            {
                GameId = 4,
                Name = "Playerunkow's Battlegrounds",
                Description = "Battleroyale",
                Genres = new List<Models.Genre> { genreSurvival, genreFPS },
                IconSource = "https://orig06.deviantart.net/31c8/f/2017/087/1/5/playerunknown_s_battlegrounds_icon_by_troublem4ker-db3tnhy.png",
                ImageSource = "https://www.windowscentral.com/sites/wpcentral.com/files/styles/xlarge/public/field/image/2017/12/pubg-press-2.jpg?itok=hEREwXIC"
            };
            yield return new Models.Game
            {
                GameId = 6,
                Name = "Dead by Daylight",
                Description = "Horror",
                Genres = new List<Models.Genre> { genreSurvivalHorror },
                IconSource = "https://www.deadbydaylight.com/manual/img/journal/dbd-journal-hook.png",
                ImageSource = "http://maroonersrock.com/wp-content/uploads/2017/10/Dead-by-Daylight-Freddy-DLC-810x400.jpg"
            };
            yield return new Models.Game
            {
                GameId = 5,
                Name = "Fortnite",
                Description = "Battleroyale",
                Genres = new List<Models.Genre> { genreSurvival, genreFPS },
                IconSource = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQyUWh_P9xq4o3Rja1PZsHA7ydEMUZXBJyYuSlxD8UIXBdBjzt_",
                ImageSource = "https://cdn2.unrealengine.com/Fortnite%2Fbattle-royale%2Ffortnite-sniper-1920x1080-f072fcef414cbe680e369a16a8d059d8a01c7636.jpg"
            };
        }

        public Task<Models.Game> GetGameAsync(int gameId)
        {
            Models.Genre genreFPS = new Models.Genre() { GenreId = 1, Name = "FPS" };
            return Task.FromResult(new Models.Game
            {
                GameId = 1,
                Name = "Rainbow Six Siege",
                Description = "FPS mapas, estratégia...",
                Genres = new List<Models.Genre> { genreFPS },
                IconSource = "https://i.redd.it/iznunq2m8vgy.png",
                ImageSource = "https://www.torcedores.com/content/uploads/2018/02/erwmqs3hzvkfqudfutqdyh_tb7c.jpg"
            });
        }

        public Task<IEnumerable<Models.Game>> GetGamesAsync()
        {
            return Task.FromResult(Games);
        }

        public Task<IEnumerable<Models.Game>> GetGamesAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Game>> GetGamesByPlatformAsync(int platformId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Game>> GetTrendGamesAsync()
        {
            return Task.FromResult(Games);
        }

        public Task<bool> PostGameAsync(Models.Game game)
        {
            return Task.FromResult(false);
        }

        public Task<bool> DeleteGameAsync(int gameId)
        {
            return Task.FromResult(false);
        }
    }
}
