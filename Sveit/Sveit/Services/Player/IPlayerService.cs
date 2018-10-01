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

        Task<bool> PostPlayerSkill(Models.PlayerSkill playerSkill);

        Task<bool> PostPlayerAsync(Models.Player player);

        Task<bool> DeletePlayerAsync(int playerId);
    }
}
