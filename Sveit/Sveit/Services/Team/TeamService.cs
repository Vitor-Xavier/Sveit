using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sveit.Extensions;
using Sveit.Services.Login;
using Sveit.Services.Requests;

namespace Sveit.Services.Team
{
    public class TeamService : ITeamService
    {
        private readonly IRequestService _requestService;

        private readonly ILoginService _loginService;

        public TeamService(IRequestService requestService)
        {
            _requestService = requestService;
            _loginService = new LoginService(_requestService);
        }

        public Task<Models.Team> GetById(int teamId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.TeamsEndpoint);
            builder.AppendToPath(teamId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<Models.Team>(uri);
        }

        public Task<IEnumerable<Models.Team>> GetByName(string name)
        {
            UriBuilder builder = new UriBuilder(AppSettings.TeamsEndpoint);
            builder.AppendToPath($"name/{name}");
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Team>>(uri);
        }

        public Task<IEnumerable<Models.Team>> GetByNameAndGame(string name, int gameId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.TeamsEndpoint);
            builder.AppendToPath($"name/{name}/{gameId}");
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Team>>(uri);
        }

        public Task<IEnumerable<Models.Team>> GetByGame(int gameId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.TeamsEndpoint);
            builder.AppendToPath($"Game/{gameId}");
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Team>>(uri);
        }

        public Task<IEnumerable<Models.Team>> GetByGameName(string gameName)
        {
            UriBuilder builder = new UriBuilder(AppSettings.TeamsEndpoint);
            builder.AppendToPath($"Game/name/{gameName}");
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Team>>(uri);
        }

        public Task<IEnumerable<Models.Team>> GetByPlayer(int playerId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.TeamsEndpoint);
            builder.AppendToPath($"Player/{playerId}");
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Team>>(uri);
        }

        public Task<IEnumerable<Models.Player>> GetPlayers(int teamId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.TeamsEndpoint);
            builder.AppendToPath($"Players/{teamId}");
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Player>>(uri);
        }

        public Task<IEnumerable<Models.Contact>> GetTeamContacts(int teamId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.TeamsEndpoint);
            builder.AppendToPath("Contacts");
            builder.AppendToPath(teamId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Contact>>(uri);
        }

        public async Task<Models.TeamPlayer> PostTeamPlayer(Models.TeamPlayer teamPlayer)
        {
            UriBuilder builder = new UriBuilder(AppSettings.TeamsEndpoint);
            builder.AppendToPath("Player");
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.PostAsync<Models.TeamPlayer, Models.TeamPlayer>(uri, teamPlayer, token);
        }

        public async Task<Models.Contact> PostTeamContact(int teamId, Models.Contact contact)
        {
            UriBuilder builder = new UriBuilder(AppSettings.TeamsEndpoint);
            builder.AppendToPath("Contact");
            builder.AppendToPath(teamId.ToString());
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.PostAsync<Models.Contact, Models.Contact>(uri, contact, token);
        }

        public async Task<Models.Team> PostTeam(Models.Team team)
        {
            UriBuilder builder = new UriBuilder(AppSettings.TeamsEndpoint);
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.PostAsync<Models.Team, Models.Team>(uri, team, token);
        }

        public async Task<bool> DeleteTeamPlayer(int teamId, int playerId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.TeamsEndpoint);
            builder.AppendToPath("Player");
            builder.AppendToPath(teamId.ToString());
            builder.AppendToPath(playerId.ToString());
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.DeleteAsync(uri, token);
        }

        public async Task<bool> DeleteTeam(int teamId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.TeamsEndpoint);
            builder.AppendToPath(teamId.ToString());
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.DeleteAsync(uri, token);
        }
    }
}
