using Sveit.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sveit.Services.Team
{
    public class FakeTeamService : ITeamService
    {
        private GamePlatform gamePlatform = new GamePlatform
        {
            Game = new Models.Game
            {
                Name = "Overwatch",
                IconSource = "https://i.imgur.com/0RIw2RB.png",
                ImageSource = "http://www.base2.com.br/wp-content/uploads/2017/04/overwatch1280jpg-6daa73_1280w.jpg"
            },
            Platform = new Models.Platform { Name = "PC" }
        };

        private GamePlatform gamePlatform2 = new GamePlatform
        {
            Game = new Models.Game
            {
                Name = "Rainbow Six Siege",
                IconSource = "https://i.redd.it/iznunq2m8vgy.png",
                ImageSource = "https://www.torcedores.com/content/uploads/2018/02/erwmqs3hzvkfqudfutqdyh_tb7c.jpg"
            },
            Platform = new Models.Platform { Name = "PC" }
        };

        public IEnumerable<Models.Team> Teams { get; set; }

        public FakeTeamService()
        {
            Teams = GetFakeTeams();
        }

        private IEnumerable<Models.Team> GetFakeTeams()
        {
            yield return new Models.Team { TeamId = 10, Name = "To do", GamePlatform = gamePlatform, IconSource = "https://image.moboplay.com/images/apk/725/com.musSharApp.myToDoList_icon.png" };
            yield return new Models.Team { Name = "Tc Esports", GamePlatform = gamePlatform, IconSource = "http://esportsobserver.com/wp-content/uploads/2015/09/Tt-eSPORTS-logo.png" };
            yield return new Models.Team { Name = "BR Control", GamePlatform = gamePlatform2, IconSource = "https://cdn2.f-cdn.com/contestentries/1366909/28870354/5b47057dd991d_thumb900.jpg" };
            yield return new Models.Team { Name = "Bar e Sorveteria Geladão", GamePlatform = gamePlatform2, IconSource = "https://img.olx.com.br/images/31/318810031713352.jpg" };
            yield return new Models.Team { Name = "Liquidos", GamePlatform = gamePlatform, IconSource = "http://swgohome.com/phpBB3/download/file.php?avatar=53_1303826764.jpg" };
        }

        private IEnumerable<Models.Player> GetFakePlayers()
        {
            yield return new Models.Player { Name = "Alguém", Nickname = "Destino", AvatarSource = "https://i.pinimg.com/736x/4a/3f/12/4a3f129ec33ab700ee2f7a022e545cdc--avatar.jpg", Gender = new Models.Gender() { Name = "Masculino" } };
            yield return new Models.Player { Name = "Alguém", Nickname = "Tião", AvatarSource = "https://images-na.ssl-images-amazon.com/images/I/41gcSWbhe0L._SY355_.jpg", Gender = new Models.Gender() { Name = "Masculino" } };
            yield return new Models.Player { Name = "Alguém", Nickname = "user001", AvatarSource = "https://2static1.fjcdn.com/comments/I+made+this+yesterday+for+steam+avatar+thought+someone+would+_350f2ad66ab4e8da63467b38a1a68db0.png", Gender = new Models.Gender { Name = "Masculino" } };
            yield return new Models.Player { Name = "Alguém", Nickname = "undefined", AvatarSource = "https://i.pinimg.com/236x/c9/0b/43/c90b43c70af2c51d92814a0b08aff527--the-phantom-avatar.jpg", Gender = new Models.Gender { Name = "Masculino" } };
            yield return new Models.Player { Name = "Alguém", Nickname = "Agilidaz", AvatarSource = "https://i.pinimg.com/736x/4a/3f/12/4a3f129ec33ab700ee2f7a022e545cdc--avatar.jpg", Gender = new Models.Gender { Name = "Masculino" } };
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
            return Task.FromResult(new Models.Team
            {
                TeamId = 10,
                Name = "To do",
                GamePlatform = gamePlatform,
                IconSource = "https://image.moboplay.com/images/apk/725/com.musSharApp.myToDoList_icon.png",
                Owner = new Models.Player { Name = "Vitor Xavier de Souza", Nickname = "vitorxs" }
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
            return Task.FromResult(team);
        }

        public Task<bool> PostTeamPlayer(TeamPlayer teamPlayer)
        {
            return Task.FromResult(false);
        }

        public Task<Models.Contact> PostTeamContact(int teamId, Models.Contact contact)
        {
            return Task.FromResult(new Models.Contact
            {
                ContactId = 1,
                Text = "user_discord",
                ContactTypeId = 1,
                ContactType = new Models.ContactType
                {
                    ContactTypeId = 1,
                    Name = "Discord"
                }
            });
        }

        public Task<IEnumerable<Models.Contact>> GetTeamContacts(int teamId)
        {
            IEnumerable<Models.Contact> contacts = new List<Models.Contact>()
            {
                new Models.Contact
                {
                    ContactId = 1,
                    Text = "todo_discord",
                    ContactTypeId = 1,
                    ContactType = new Models.ContactType
                    {
                        ContactTypeId = 1,
                        Name = "Discord"
                    }
                },
                new Models.Contact
                {
                    ContactId = 2,
                    Text = "todo_skype",
                    ContactTypeId = 1,
                    ContactType = new Models.ContactType
                    {
                        ContactTypeId = 2,
                        Name = "Skype"
                    }
                }
            };
            return Task.FromResult(contacts);
        }

    }
}
