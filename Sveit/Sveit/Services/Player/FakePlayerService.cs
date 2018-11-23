using Sveit.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sveit.Services.Player
{
    public class FakePlayerService : IPlayerService
    {
        private readonly GamePlatform gamePlatform = new GamePlatform
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
            GamePlatform gamePlatform = new GamePlatform
            {
                Game = new Models.Game { Name = "Overwatch" },
                Platform = new Models.Platform { Name = "PC" }
            };
            GamePlatform gamePlatform2 = new GamePlatform
            {
                Game = new Models.Game { Name = "Rainbow Six Siege" },
                Platform = new Models.Platform { Name = "PC" }
            };

            yield return new Models.Team { TeamId = 10, Name = "To do", GamePlatform = gamePlatform, IconSource = "https://image.moboplay.com/images/apk/725/com.musSharApp.myToDoList_icon.png" };
            yield return new Models.Team { Name = "Tc Esports", GamePlatform = gamePlatform, IconSource = "http://esportsobserver.com/wp-content/uploads/2015/09/Tt-eSPORTS-logo.png" };
            yield return new Models.Team { Name = "BR Control", GamePlatform = gamePlatform2, IconSource = "https://cdn2.f-cdn.com/contestentries/1366909/28870354/5b47057dd991d_thumb900.jpg" };
            yield return new Models.Team { Name = "Bar e Sorveteria Geladão", GamePlatform = gamePlatform2, IconSource = "https://img.olx.com.br/images/31/318810031713352.jpg" };
            yield return new Models.Team { Name = "Liquidos", GamePlatform = gamePlatform, IconSource = "http://swgohome.com/phpBB3/download/file.php?avatar=53_1303826764.jpg" };
        }

        public Task<bool> DeletePlayerAsync(int playerId)
        {
            return Task.FromResult(false);
        }

        public Task<Models.Player> GetPlayerById(int playerId)
        {
            return Task.FromResult(new Models.Player
            {
                Name = "Vitor Xavier de Souza",
                Nickname = "vitorxs",
                Gender = new Models.Gender { GenderId = 1, Name = "Masculino" },
                AvatarSource = "https://i.pinimg.com/originals/c8/0a/09/c80a0933df51f9f5be92d033c6db65b2.jpg",
                Deleted = false,
                DateOfBirth = new System.DateTime(1997, 01, 06),
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

        public Task<Models.Player> UpdatePlayer(Models.Player player)
        {
            return Task.FromResult(player);
        }

        public Task<Models.Player> PostPlayerAsync(Models.Player player)
        {
            return Task.FromResult(player);
        }

        public Task<Models.PlayerSkill> PostPlayerSkill(Models.PlayerSkill playerSkill)
        {
            return Task.FromResult(playerSkill);
        }

        public Task<Models.Contact> PostPlayerContact(int playerId, Models.Contact contact)
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

        public Task<IEnumerable<Models.Contact>> GetPlayerContacts(int playerId)
        {
            IEnumerable<Models.Contact> contacts = new List<Models.Contact>()
            {
                new Models.Contact
                {
                    ContactId = 1,
                    Text = "user_discord",
                    ContactTypeId = 1,
                    ContactType = new Models.ContactType
                    {
                        ContactTypeId = 1,
                        Name = "Discord",
                        IconSource = "ic_discord.png"
                    }
                },
                new Models.Contact
                {
                    ContactId = 2,
                    Text = "user_skype",
                    ContactTypeId = 1,
                    ContactType = new Models.ContactType
                    {
                        ContactTypeId = 2,
                        Name = "Skype",
                        IconSource = "ic_skype.png"
                    }
                }
            };
            return Task.FromResult(contacts);
        }

        public Task<bool> DeleteTeamPlayer(int playerId, int teamId)
        {
            return Task.FromResult(false);
        }
    }
}
