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
            yield return new Models.Player { Name = "Alguém", Nickname = "Fate", AvatarSource = "https://i.pinimg.com/736x/4a/3f/12/4a3f129ec33ab700ee2f7a022e545cdc--avatar.jpg", Gender = new Models.Gender() { Name = "Masculino" } };
            yield return new Models.Player { Name = "Alguém", Nickname = "envy", AvatarSource = "https://i.pinimg.com/236x/c9/0b/43/c90b43c70af2c51d92814a0b08aff527--the-phantom-avatar.jpg", Gender = new Models.Gender() { Name = "Masculino" } };
            yield return new Models.Player { Name = "Alguém", Nickname = "Verbo", AvatarSource = "https://i.pinimg.com/736x/4a/3f/12/4a3f129ec33ab700ee2f7a022e545cdc--avatar.jpg", Gender = new Models.Gender { Name = "Masculino" } };
            yield return new Models.Player { Name = "Alguém", Nickname = "KariV", AvatarSource = "https://i.pinimg.com/236x/c9/0b/43/c90b43c70af2c51d92814a0b08aff527--the-phantom-avatar.jpg", Gender = new Models.Gender { Name = "Masculino" } };
            yield return new Models.Player { Name = "Alguém", Nickname = "GrimReality", AvatarSource = "https://i.pinimg.com/736x/4a/3f/12/4a3f129ec33ab700ee2f7a022e545cdc--avatar.jpg", Gender = new Models.Gender { Name = "Masculino" } };
            yield return new Models.Player { Name = "Alguém", Nickname = "Agilities", AvatarSource = "https://i.pinimg.com/236x/c9/0b/43/c90b43c70af2c51d92814a0b08aff527--the-phantom-avatar.jpg", Gender = new Models.Gender { Name = "Masculino" } };
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
            var gamePlatform = new GamePlatform
            {
                Game = new Models.Game { Name = "Overwatch", ImageSource = "http://fullhdpictures.com/wp-content/uploads/2016/03/Magnificent-Overwatch-Wallpaper.png", IconSource = "http://esportsobserver.com/wp-content/uploads/2015/09/Tt-eSPORTS-logo.png" },
                Platform = new Models.Platform { Name = "PC" }
            };

            return Task.FromResult(new Models.Team
            {
                Name = "Immortals",
                GamePlatform = gamePlatform,
                IconSource = "http://liquipedia.net/commons/images/thumb/1/1d/Immortals_org.png/600px-Immortals_org.png",
                Owner = new Models.Player { Name = "Owner player", Nickname = "Owner_" }
            });
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
