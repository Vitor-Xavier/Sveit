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
                GameId = 2,
                Game = new Models.Game
                {
                    GameId = 2,
                    Name = "Overwatch",
                    IconSource = "https://i.imgur.com/0RIw2RB.png",
                    ImageSource = "http://www.base2.com.br/wp-content/uploads/2017/04/overwatch1280jpg-6daa73_1280w.jpg"
                },
                Platform = new Models.Platform { Name = "PC" }
            };
            var gamePlatform1 = new GamePlatform
            {
                GameId = 1,
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
                Team = new Models.Team
                {
                    TeamId = 10,
                    Name = "To do",
                    GamePlatform = gamePlatform,
                    IconSource = "https://image.moboplay.com/images/apk/725/com.musSharApp.myToDoList_icon.png"
                },
                MinAge = 16,
                MaxAge = 23,
                Description = "Descrição completa da vaga que está sendo oferecida pela equipe e alguns de seus requisitos explicados pelo membro que oferta tal vaga, além de questões comportamentais que estão a procura nos cadidatos e etc.",
                VacancyId = 1,
                Roles = new List<Models.Role>
                {
                    new Models.Role
                    {
                        Name = "Agressivo",
                        RoleType = new Models.RoleType
                        {
                            RoleTypeId = 1,
                            Name = "Papel primário",
                            IconSource = "ic_primaryRole.png"
                        }
                    },
                    new Models.Role
                    {
                        Name = "DPS",
                        RoleType = new Models.RoleType
                        {
                            RoleTypeId = 2,
                            Name = "Papel secundário",
                            IconSource = "ic_secondaryRole.png"
                        }
                    }, new Models.Role
                    {
                        Name = "Tank",
                        RoleType = new Models.RoleType
                        {
                            RoleTypeId = 2,
                            Name = "Papel secundário",
                            IconSource = "ic_secondaryRole.png"
                        }
                    }
                },
                Skills = new List<Models.Skill> { new Models.Skill { Name = "tiro" }, new Models.Skill { Name = "healing" }, new Models.Skill { Name = "suporte" }, new Models.Skill { Name = "sniper" }, new Models.Skill { Name = "estratégia" } }
            };
            yield return new Models.Vacancy
            {
                Title = "Vaga tanque",
                Team = new Models.Team { Name = "Tc Esports", GamePlatform = gamePlatform, IconSource = "http://esportsobserver.com/wp-content/uploads/2015/09/Tt-eSPORTS-logo.png" },
                VacancyId = 2,
                Description = "vaga...."
            };
            yield return new Models.Vacancy
            {
                Title = "Vaga dps",
                Team = new Models.Team { Name = "Tc Esports", GamePlatform = gamePlatform, IconSource = "http://esportsobserver.com/wp-content/uploads/2015/09/Tt-eSPORTS-logo.png" },
                VacancyId = 3,
                Description = "vaga...."
            };
            yield return new Models.Vacancy
            {
                Title = "Vaga suporte",
                Team = new Models.Team { Name = "Commander eSports2", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                VacancyId = 4,
                Description = "vaga...."
            };
            yield return new Models.Vacancy
            {
                Title = "Vaga suporte",
                Team = new Models.Team { Name = "Commander eSports2", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                VacancyId = 5,
                Description = "vaga...."
            };
            yield return new Models.Vacancy
            {
                Title = "Vaga suporte",
                Team = new Models.Team { Name = "Commander eSports2", GamePlatform = gamePlatform1, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                VacancyId = 6,
                Description = "vaga...."
            };
            yield return new Models.Vacancy
            {
                Title = "Vaga suporte",
                Team = new Models.Team { Name = "Commander eSports2", GamePlatform = gamePlatform1, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                VacancyId = 7,
                Description = "vaga...."
            };
        }

        public Task<bool> DeleteVacancyAsync(int vacancyId)
        {
            return Task.FromResult(false);
        }

        public Task<IEnumerable<Models.Vacancy>> GetVacanciesByGameAsync(int gameId)
        {
            return Task.FromResult(Vacancies.Where(v => v.Team.GamePlatform_GameId == gameId));
        }

        public Task<IEnumerable<Models.Vacancy>> GetVacanciesByTeamAsync(int teamId)
        {
            return Task.FromResult(new List<Models.Vacancy> { Vacancies.First() }.AsEnumerable());
            //return Task.FromResult(Vacancies.Where(v => v.TeamId == teamId));
        }

        public Task<Models.Vacancy> GetVacancyAsync(int vacancyId)
        {
            var gamePlatform = new GamePlatform
            {
                Game = new Models.Game
                {
                    Name = "Overwatch",
                    IconSource = "https://i.imgur.com/0RIw2RB.png",
                    ImageSource = "http://www.base2.com.br/wp-content/uploads/2017/04/overwatch1280jpg-6daa73_1280w.jpg"
                },
                Platform = new Models.Platform { Name = "PC" }
            };

            return Task.FromResult(new Models.Vacancy
            {
                Title = "Vaga suporte",
                Team = new Models.Team
                {
                    Name = "To do",
                    GamePlatform = gamePlatform,
                    IconSource = "https://image.moboplay.com/images/apk/725/com.musSharApp.myToDoList_icon.png"
                },
                MinAge = 16,
                MaxAge = 23,
                Description = "Descrição completa da vaga que está sendo oferecida pela equipe e alguns de seus requisitos explicados pelo membro que oferta tal vaga, além de questões comportamentais que estão a procura nos cadidatos e etc.",
                VacancyId = 1,
                Roles = new List<Models.Role> { new Models.Role { Name = "Agressivo", RoleType = new Models.RoleType { RoleTypeId = 1, Name = "Papel primário" } }, new Models.Role { Name = "DPS", RoleType = new Models.RoleType { RoleTypeId = 2, Name = "Papel secundário" } }, new Models.Role { Name = "Tank", RoleType = new Models.RoleType { RoleTypeId = 2, Name = "Papel secundário" } } },
                Skills = new List<Models.Skill> { new Models.Skill { Name = "tiro" }, new Models.Skill { Name = "healing" }, new Models.Skill { Name = "suporte" }, new Models.Skill { Name = "sniper" }, new Models.Skill { Name = "estratégia" } }
            });
        }

        public Task<Models.Vacancy> PostVacancyAsync(Models.Vacancy vacancy)
        {
            return Task.FromResult(vacancy);
        }
    }
}
