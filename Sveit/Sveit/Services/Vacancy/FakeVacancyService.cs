using Sveit.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sveit.Services.Vacancy
{
    public class FakeVacancyService : IVacancyService
    {
        public IEnumerable<Models.Vacancy> Vacancies { get; private set; }

        public FakeVacancyService()
        {
            Vacancies = GetFakeVacancies();
        }

        private IEnumerable<Models.Vacancy> GetFakeVacancies()
        {
            var gamePlatform = new GamePlatform
            {
                Game = new Models.Game { GameId = 2, Name = "Overwatch", ImageSource = "http://fullhdpictures.com/wp-content/uploads/2016/03/Magnificent-Overwatch-Wallpaper.png", IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                Platform = new Models.Platform { Name = "PC" }
            };
            var gamePlatform1 = new GamePlatform
            {
                Game = new Models.Game
                {
                    GameId = 1,
                    Name = "Rainbow Six Siege",
                    IconSource = "https://i.redd.it/iznunq2m8vgy.png",
                    ImageSource = "https://www.torcedores.com/content/uploads/2018/02/erwmqs3hzvkfqudfutqdyh_tb7c.jpg"
                },
                Platform = new Models.Platform { Name = "PC" }
            };
            yield return new Models.Vacancy
            {
                Title = "Vaga suporte",
                Skills = new List<Models.Skill> { new Models.Skill { Name = "skill1" }, new Models.Skill { Name = "skill1" }, new Models.Skill { Name = "skill1" } },
                Team = new Models.Team { Name = "Commander eSports", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                VacancyId = 1,
                Description = "vaga...."
            };
            yield return new Models.Vacancy
            {
                Title = "Vaga suporte",
                Team = new Models.Team { Name = "Commander eSports2", GamePlatform = gamePlatform1, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                VacancyId = 1,
                Description = "vaga...."
            };
            yield return new Models.Vacancy
            {
                Title = "Vaga suporte",
                Team = new Models.Team { Name = "Commander eSports2", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                VacancyId = 1,
                Description = "vaga...."
            };
            yield return new Models.Vacancy
            {
                Title = "Vaga suporte",
                Team = new Models.Team { Name = "Commander eSports2", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                VacancyId = 1,
                Description = "vaga...."
            };
            yield return new Models.Vacancy
            {
                Title = "Vaga suporte",
                Team = new Models.Team { Name = "Commander eSports2", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                VacancyId = 1,
                Description = "vaga...."
            };
            yield return new Models.Vacancy
            {
                Title = "Vaga suporte",
                Team = new Models.Team { Name = "Commander eSports2", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                VacancyId = 1,
                Description = "vaga...."
            };
            yield return new Models.Vacancy
            {
                Title = "Vaga suporte",
                Team = new Models.Team { Name = "Commander eSports2", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                VacancyId = 1,
                Description = "vaga...."
            };
        }

        public Task<bool> DeleteVacancyAsync(int vacancyId)
        {
            return Task.FromResult(false);
        }

        public Task<IEnumerable<Models.Vacancy>> GetVacanciesByGameAsync(int gameId)
        {
            return Task.FromResult(Vacancies);
        }

        public Task<IEnumerable<Models.Vacancy>> GetVacanciesByTeamAsync(int teamId)
        {
            return Task.FromResult(Vacancies);
        }

        public Task<Models.Vacancy> GetVacancyAsync(int vacancyId)
        {
            var gamePlatform = new GamePlatform
            {
                Game = new Models.Game { Name = "Overwatch", ImageSource = "http://fullhdpictures.com/wp-content/uploads/2016/03/Magnificent-Overwatch-Wallpaper.png", IconSource = "http://esportsobserver.com/wp-content/uploads/2015/09/Tt-eSPORTS-logo.png" },
                Platform = new Models.Platform { Name = "PC" }
            };

            return Task.FromResult(new Models.Vacancy
            {
                Title = "Vaga suporte",
                Team = new Models.Team
                {
                    Name = "Immortals",
                    GamePlatform = gamePlatform,
                    IconSource = "http://liquipedia.net/commons/images/thumb/1/1d/Immortals_org.png/600px-Immortals_org.png",
                    Owner = new Models.Player { Name = "Owner player", Nickname = "Owner_" },
                },
                MinAge = 16,
                MaxAge = 23,
                Description = "Descrição completa da vaga que está sendo oferecida pela equipe e alguns de seus requisitos explicados pelo membro que oferta tal vaga, além de questões comportamentais que estão a procura nos cadidatos e etc.",
                VacancyId = 1,
                Skills = new List<Models.Skill> { new Models.Skill { Name = "tiro" }, new Models.Skill { Name = "healing" }, new Models.Skill { Name = "suporte" }, new Models.Skill { Name = "sniper" }, new Models.Skill { Name = "estratégia" } }
            });
        }

        public Task<bool> PostVacancyAsync(Models.Vacancy vacancy)
        {
            return Task.FromResult(false);
        }
    }
}
