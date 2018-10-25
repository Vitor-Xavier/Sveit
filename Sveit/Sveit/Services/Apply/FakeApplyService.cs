using Sveit.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sveit.Services.Apply
{
    public class FakeApplyService : IApplyService
    {
        public IEnumerable<Models.Apply> Applies { get; private set; }

        public FakeApplyService()
        {
            Applies = GetFakeApplies();
        }

        private IEnumerable<Models.Apply> GetFakeApplies()
        {
            var gamePlatform = new GamePlatform
            {
                Game = new Models.Game { GameId = 2, Name = "Overwatch", ImageSource = "http://fullhdpictures.com/wp-content/uploads/2016/03/Magnificent-Overwatch-Wallpaper.png", IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                Platform = new Models.Platform { Name = "PC" }
            };
            yield return new Models.Apply
            {
                ApplyId = 1,
                Text = "Texto para ser aprovado na vaga.",
                PlayerId = 1,
                Player = new Models.Player
                {
                    Name = "Jogador 1",
                    Nickname = "PlayerOne",
                    DateOfBirth = new System.DateTime(1999, 12, 10),
                    Gender = new Models.Gender { Name = "Masculino" },
                    AvatarSource = "https://pbs.twimg.com/profile_images/949941380259991552/C4b4NckD_400x400.jpg",
                    Deleted = false,
                    PlayerId = 1
                },
                VacancyId = 1,
                Vacancy = new Models.Vacancy
                {
                    Title = "Vaga suporte, papeis",
                    Roles = new List<Models.Role> { new Models.Role { Name = "Agressivo", RoleType = new Models.RoleType { RoleTypeId = 1, Name = "Papel primário" } }, new Models.Role { Name = "DPS", RoleType = new Models.RoleType { RoleTypeId = 2, Name = "Papel secundário" } }, new Models.Role { Name = "Tank", RoleType = new Models.RoleType { RoleTypeId = 2, Name = "Papel secundário" } } },
                    Skills = new List<Models.Skill> { new Models.Skill { Name = "skill1" }, new Models.Skill { Name = "skill1" }, new Models.Skill { Name = "skill1" } },
                    Team = new Models.Team { Name = "Commander eSports", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                    VacancyId = 1,
                    Description = "vaga...."
                },
                Roles = new List<Models.Role> { new Models.Role { Name = "Agressivo", RoleType = new Models.RoleType { RoleTypeId = 1, Name = "Papel primário" } }, new Models.Role { Name = "Tank", RoleType = new Models.RoleType { RoleTypeId = 2, Name = "Papel secundário" } } },
                Approved = null
            };
            yield return new Models.Apply
            {
                ApplyId = 2,
                Text = "Texto para ser aprovado na vaga.",
                PlayerId = 1,
                Player = new Models.Player
                {
                    Name = "Jogador 1",
                    Nickname = "PlayerOne",
                    Gender = new Models.Gender { Name = "Masculino" },
                    AvatarSource = "https://pbs.twimg.com/profile_images/949941380259991552/C4b4NckD_400x400.jpg",
                    Deleted = false,
                    PlayerId = 1
                },
                VacancyId = 1,
                Vacancy = new Models.Vacancy
                {
                    Title = "Vaga suporte, papeis",
                    Roles = new List<Models.Role> { new Models.Role { Name = "Agressivo", RoleType = new Models.RoleType { RoleTypeId = 1, Name = "Papel primário" } }, new Models.Role { Name = "DPS", RoleType = new Models.RoleType { RoleTypeId = 2, Name = "Papel secundário" } }, new Models.Role { Name = "Tank", RoleType = new Models.RoleType { RoleTypeId = 2, Name = "Papel secundário" } } },
                    Skills = new List<Models.Skill> { new Models.Skill { Name = "skill1" }, new Models.Skill { Name = "skill1" }, new Models.Skill { Name = "skill1" } },
                    Team = new Models.Team { Name = "Commander eSports", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                    VacancyId = 1,
                    Description = "vaga...."
                },
                Roles = new List<Models.Role> { new Models.Role { Name = "Agressivo", RoleType = new Models.RoleType { RoleTypeId = 1, Name = "Papel primário" } } },
                Approved = true
            };
        }

        public Task<Models.Apply> GetApply(int applyId)
        {
            return Task.FromResult(Applies.First());
        }

        public Task<IEnumerable<Models.Apply>> GetAppliesByPlayer(int playerId)
        {
            return Task.FromResult(Applies);
        }

        public Task<IEnumerable<Models.Apply>> GetAppliesByVacancy(int vacancyId)
        {
            return Task.FromResult(Applies);
        }

        public Task<Models.Apply> PostApply(Models.Apply apply)
        {
            return Task.FromResult(Applies.First());
        }

        public Task<bool> DeleteApply(int applyId)
        {
            return Task.FromResult(false);
        }
    }
}
