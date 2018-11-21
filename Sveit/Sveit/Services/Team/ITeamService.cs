using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.Services.Team
{
    interface ITeamService
    {
        Task<Models.Team> GetById(int teamId);

        Task<IEnumerable<Models.Team>> GetByName(string name);

        Task<IEnumerable<Models.Team>> GetByNameAndGame(string name, int gameId);

        Task<IEnumerable<Models.Team>> GetByGame(int gameId);

        Task<IEnumerable<Models.Team>> GetByGameName(string gameName);

        Task<IEnumerable<Models.Player>> GetPlayers(int teamId);

        Task<IEnumerable<Models.Team>> GetByPlayer(int playerId);

        Task<IEnumerable<Models.Contact>> GetTeamContacts(int teamId);

        Task<Models.Contact> PostTeamContact(int teamId, Models.Contact contact);

        Task<Models.TeamPlayer> PostTeamPlayer(Models.TeamPlayer teamPlayer);

        Task<Models.Team> PostTeam(Models.Team team);

        Task<bool> DeleteTeamPlayer(int teamId, int playerId);

        Task<bool> DeleteTeam(int teamId);
    }
}
