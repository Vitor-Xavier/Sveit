using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sveit.Models;

namespace Sveit.Services.Player
{
    public class FakePlayerService : IPlayerService
    { 
        private GamePlatform gamePlatform = new GamePlatform
        {
            Game = new Models.Game { Name = "Overwatch" },
            Platform = new Models.Platform { Name = "PC" }
        };

        private IEnumerable<Models.Skill> GetFakeSkills()
        {
            yield return new Models.Skill { Name = "sniper" };
            yield return new Models.Skill { Name = "posicionamento" };
            yield return new Models.Skill { Name = "distância" };
            yield return new Models.Skill { Name = "estratégia" };
            yield return new Models.Skill { Name = "pontaria" };
        }

        private IEnumerable<Models.Player> GetFakePlayers()
        {
            yield return new Models.Player
            {
                Name = "Jogador 1",
                Nickname = "PlayerOne",
                Gender = new Models.Gender { Name = "Masculino" },
                AvatarSource = "https://pbs.twimg.com/profile_images/949941380259991552/C4b4NckD_400x400.jpg",
                Deleted = false,
                PlayerId = 1
            };
            yield return new Models.Player
            {
                Name = "Jogador 2",
                Nickname = "PlayerTwo",
                Gender = new Models.Gender { Name = "Masculino" },
                AvatarSource = "https://pbs.twimg.com/profile_images/949941380259991552/C4b4NckD_400x400.jpg",
                Deleted = false,
                PlayerId = 2
            };
        }

        private IEnumerable<Models.Team> GetFakeTeams()
        {
            yield return new Models.Team { Name = "Commander eSports1", GamePlatform = gamePlatform, IconSource = "http://esportsobserver.com/wp-content/uploads/2015/09/Tt-eSPORTS-logo.png" };
            yield return new Models.Team { Name = "Commander eSports2", GamePlatform = gamePlatform, IconSource = "http://esportsobserver.com/wp-content/uploads/2015/09/Tt-eSPORTS-logo.png" };
            yield return new Models.Team { Name = "Commander eSports3", GamePlatform = gamePlatform, IconSource = "http://esportsobserver.com/wp-content/uploads/2015/09/Tt-eSPORTS-logo.png" };
            yield return new Models.Team { Name = "Commander eSports4", GamePlatform = gamePlatform, IconSource = "http://esportsobserver.com/wp-content/uploads/2015/09/Tt-eSPORTS-logo.png" };
            yield return new Models.Team { Name = "Commander eSports5", GamePlatform = gamePlatform, IconSource = "http://esportsobserver.com/wp-content/uploads/2015/09/Tt-eSPORTS-logo.png" };
            yield return new Models.Team { Name = "Commander eSports6", GamePlatform = gamePlatform, IconSource = "http://esportsobserver.com/wp-content/uploads/2015/09/Tt-eSPORTS-logo.png" };
            yield return new Models.Team { Name = "Commander eSports7", GamePlatform = gamePlatform, IconSource = "http://esportsobserver.com/wp-content/uploads/2015/09/Tt-eSPORTS-logo.png" };
        }

        public Task<bool> DeletePlayerAsync(int playerId)
        {
            return Task.FromResult(false);
        }

        public Task<Models.Player> GetPlayerById(int playerId)
        {
            return Task.FromResult(new Models.Player
            {
                Name = "Jogador 2",
                Nickname = "PlayerTwo",
                Gender = new Models.Gender { Name = "Masculino" },
                AvatarSource = "https://pbs.twimg.com/profile_images/949941380259991552/C4b4NckD_400x400.jpg",
                Deleted = false,
                PlayerId = 2
            });
        }

        public Task<IEnumerable<Models.Player>> GetPlayerByName(string name)
        {
            return Task.FromResult(GetFakePlayers());
        }

        public Task<IEnumerable<Models.Team>> GetPlayerTeams(int playerId)
        {
            return Task.FromResult(GetFakeTeams());
        }

        public Task<IEnumerable<Models.Skill>> GetSkills(int playerId)
        {
            return Task.FromResult(GetFakeSkills());
        }

        public Task<bool> PostPlayerAsync(Models.Player player)
        {
            return Task.FromResult(false);
        }

        public Task<bool> PostPlayerSkill(PlayerSkill playerSkill)
        {
            return Task.FromResult(false);
        }
    }
}
