using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.Services.Player
{
    public interface IPlayerService
    {
        Task<Models.Player> GetPlayerById(int playerId);

        Task<IEnumerable<Models.Player>> GetPlayerByName(string name);

        Task<IEnumerable<Models.Team>> GetPlayerTeams(int playerId);

        Task<IEnumerable<Models.Skill>> GetSkills(int playerId);

        Task<IEnumerable<Models.Contact>> GetPlayerContacts(int playerId);

        Task<bool> PostPlayerSkill(Models.PlayerSkill playerSkill);

        Task<Models.Contact> PostPlayerContact(int playerId, Models.Contact contact);

        Task<Models.Player> PostPlayerAsync(Models.Player player);

        Task<bool> DeletePlayerAsync(int playerId);
    }
}
