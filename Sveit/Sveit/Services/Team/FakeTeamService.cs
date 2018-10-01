using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sveit.Models;

namespace Sveit.Services.Team
{
    public class FakeTeamService : ITeamService
    {
        private GamePlatform gamePlatform = new GamePlatform
        {
            Game = new Models.Game { Name = "Overwatch" },
            Platform = new Models.Platform { Name = "PC" }
        };

        public IEnumerable<Models.Team> Teams { get; set; }

        public FakeTeamService()
        {
            Teams = GetFakeTeams();
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

        public Task<bool> DeleteTeam(int teamId)
        {
            return Task.FromResult(false);
        }

        public Task<IEnumerable<Models.Team>> GetByGame(int gameId)
        {
            return Task.FromResult(Teams);
        }

        public Task<IEnumerable<Models.Team>> GetByGameName(string gameName)
        {
            return Task.FromResult(Teams);
        }

        public Task<Models.Team> GetById(int teamId)
        {
            return Task.FromResult(new Models.Team { Name = "Commander eSports1", GamePlatform = gamePlatform, IconSource = "http://esportsobserver.com/wp-content/uploads/2015/09/Tt-eSPORTS-logo.png" });
        }

        public Task<IEnumerable<Models.Team>> GetByName(string name)
        {
            return Task.FromResult(Teams);
        }

        public Task<IEnumerable<Models.Team>> GetByNameAndGame(string name, int gameId)
        {
            return Task.FromResult(Teams);
        }

        public Task<IEnumerable<Models.Team>> GetByPlayer(int playerId)
        {
            return Task.FromResult(Teams);
        }

        public Task<IEnumerable<Models.Player>> GetPlayers(int teamId)
        {
            IEnumerable<Models.Player> players = GetFakePlayers();
            return Task.FromResult(players);
        }

        public Task<Models.Team> PostTeam(Models.Team team)
        {
            return Task.FromResult(new Models.Team { Name = "Commander eSports1", GamePlatform = gamePlatform, IconSource = "http://esportsobserver.com/wp-content/uploads/2015/09/Tt-eSPORTS-logo.png" });
        }

        public Task<bool> PostTeamPlayer(TeamPlayer teamPlayer)
        {
            return Task.FromResult(false);
        }
    }
}
