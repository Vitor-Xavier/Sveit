using Sveit.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Sveit.Services.Vacancy;
using Sveit.Services.Requests;
using Sveit.Services.Game;

namespace Sveit.ViewModels
{
    public class VacanciesViewModel : BindableObject
    {
        public ObservableCollection<Vacancy> Vacancys { get; set; }

        public ObservableCollection<Vacancy> Vacancies { get; set; }

        public ObservableCollection<Game> Games { get; set; }

        private readonly IVacancyService _vacancyService;

        private readonly IGameService _gameService;

        private int position = 0;

        public int Position
        {
            get { return position; }
            set
            {
                position = value;
                LoadByGame(position, true);
                OnPropertyChanged();
            }
        }

        public VacanciesViewModel()
        {
            var requestService = new RequestService();
            _vacancyService = new VacancyService(requestService);
            _gameService = new GameService(requestService);
            Games = new ObservableCollection<Game>();
            Vacancies = new ObservableCollection<Vacancy>();
            Vacancys = new ObservableCollection<Vacancy>();
            LoadGames(true);
        }

        private async void LoadByGame(int pos)
        {
            var vacancies = await _vacancyService.GetVacanciesByGameAsync(Games[pos].GameId);
            Vacancies.Clear();
            foreach (Vacancy vacancy in vacancies)
                Vacancies.Add(vacancy);
        }

        private void LoadByGame(int pos, bool local)
        {
            var vacancys = Vacancys.Where(v => v.Team.GamePlatform.Game.GameId == Games[pos].GameId);
            Vacancies.Clear();
            foreach (Vacancy v in vacancys)
                Vacancies.Add(v);
        }

        private async void LoadGames()
        {
            var games = await _gameService.GetTrendGamesAsync();

            Games.Clear();
            foreach (Game game in games)
                Games.Add(game);

            if (Games.Count > 0)
                Position = 0;
        }

        private void LoadGames(bool local)
        {
            Games.Clear();
            Genre genreFPS = new Genre() { GenreId = 1, Name = "FPS" };
            Genre genreAction = new Genre() { GenreId = 1, Name = "Action" };
            Genre genreSurvival = new Genre() { GenreId = 5, Name = "Survival" };
            Genre genreSurvivalHorror = new Genre { GenreId = 6, Name = "Survival Horror" };
            Games.Add(new Game
            {
                GameId = 1,
                Name = "Rainbow Six Siege",
                Description = "FPS mapas, estratégia...",
                Genres = new List<Genre> { genreFPS },
                IconSource = "https://i.redd.it/iznunq2m8vgy.png",
                ImageSource = "https://www.torcedores.com/content/uploads/2018/02/erwmqs3hzvkfqudfutqdyh_tb7c.jpg"
            });
            Games.Add(new Game
            {
                GameId = 2,
                Name = "Overwatch",
                Description = "FPS, objetivo, 5 membros",
                Genres = new List<Genre> { genreFPS },
                IconSource = "https://i.imgur.com/0RIw2RB.png",
                ImageSource = "http://www.base2.com.br/wp-content/uploads/2017/04/overwatch1280jpg-6daa73_1280w.jpg"
            });
            Games.Add(new Game
            {
                GameId = 3,
                Name = "Destiny 2",
                Description = "FPS ",
                Genres = new List<Genre> { genreFPS },
                IconSource = "http://i44.servimg.com/u/f44/13/94/19/07/destin10.png",
                ImageSource = "https://www.pcguia.pt/wp-content/uploads/2018/02/Destiny-2-900x445.jpg"
            });
            Games.Add(new Game
            {
                GameId = 4,
                Name = "Playerunkow's Battlegrounds",
                Description = "Battleroyale",
                Genres = new List<Genre> { genreSurvival, genreFPS },
                IconSource = "https://orig06.deviantart.net/31c8/f/2017/087/1/5/playerunknown_s_battlegrounds_icon_by_troublem4ker-db3tnhy.png",
                ImageSource = "https://www.windowscentral.com/sites/wpcentral.com/files/styles/xlarge/public/field/image/2017/12/pubg-press-2.jpg?itok=hEREwXIC"
            });
            Games.Add(new Game
            {
                GameId = 6,
                Name = "Dead by Daylight",
                Description = "Horror",
                Genres = new List<Genre> { genreSurvivalHorror },
                IconSource = "https://www.deadbydaylight.com/manual/img/journal/dbd-journal-hook.png",
                ImageSource = "http://maroonersrock.com/wp-content/uploads/2017/10/Dead-by-Daylight-Freddy-DLC-810x400.jpg"
            });
            Games.Add(new Game
            {
                GameId = 5,
                Name = "Fortnite",
                Description = "Battleroyale",
                Genres = new List<Genre> { genreSurvival, genreFPS },
                IconSource = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQyUWh_P9xq4o3Rja1PZsHA7ydEMUZXBJyYuSlxD8UIXBdBjzt_",
                ImageSource = "https://cdn2.unrealengine.com/Fortnite%2Fbattle-royale%2Ffortnite-sniper-1920x1080-f072fcef414cbe680e369a16a8d059d8a01c7636.jpg"
            });
            LoadVacancies();
            if (Games.Count > 0)
                Position = 0;
        }

        private void LoadVacancies()
        {
            var gamePlatform = new GamePlatform
            {
                Game = new Game { GameId = 2, Name = "Overwatch", ImageSource = "http://fullhdpictures.com/wp-content/uploads/2016/03/Magnificent-Overwatch-Wallpaper.png", IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                Platform = new Platform { Name = "PC" }
            };
            var gamePlatform1 = new GamePlatform
            {
                Game = new Game
                {
                    GameId = 1,
                    Name = "Rainbow Six Siege",
                    IconSource = "https://i.redd.it/iznunq2m8vgy.png",
                    ImageSource = "https://www.torcedores.com/content/uploads/2018/02/erwmqs3hzvkfqudfutqdyh_tb7c.jpg"
                },
                Platform = new Platform { Name = "PC" }
            };
            Vacancys.Clear();
            Vacancys.Add(new Vacancy
            {
                Title = "Vaga suporte",
                Skills = new List<Skill> { new Skill { Name = "skill1" }, new Skill { Name = "skill1" }, new Skill { Name = "skill1" } },
                Team = new Team { Name = "Commander eSports", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                VacancyId = 1,
                Description = "vaga...."
            });
            Vacancys.Add(new Vacancy
            {
                Title = "Vaga suporte",
                Team = new Team { Name = "Commander eSports2", GamePlatform = gamePlatform1, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                VacancyId = 1,
                Description = "vaga...."
            });
            Vacancys.Add(new Vacancy
            {
                Title = "Vaga suporte",
                Team = new Team { Name = "Commander eSports2", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                VacancyId = 1,
                Description = "vaga...."
            });
            Vacancys.Add(new Vacancy
            {
                Title = "Vaga suporte",
                Team = new Team { Name = "Commander eSports2", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                VacancyId = 1,
                Description = "vaga...."
            });
            Vacancys.Add(new Vacancy
            {
                Title = "Vaga suporte",
                Team = new Team { Name = "Commander eSports2", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                VacancyId = 1,
                Description = "vaga...."
            });
            Vacancys.Add(new Vacancy
            {
                Title = "Vaga suporte",
                Team = new Team { Name = "Commander eSports2", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                VacancyId = 1,
                Description = "vaga...."
            });
            Vacancys.Add(new Vacancy
            {
                Title = "Vaga suporte",
                Team = new Team { Name = "Commander eSports2", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                VacancyId = 1,
                Description = "vaga...."
            });
        }
    }
}
