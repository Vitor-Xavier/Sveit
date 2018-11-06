using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Login;
using Sveit.Services.Requests;

namespace Sveit.Services.Player
{
    class PlayerService : IPlayerService
    {
        private readonly IRequestService _requestService;

        private readonly ILoginService _loginService;

        public PlayerService(IRequestService requestService)
        {
            _requestService = requestService;
            _loginService = new LoginService(_requestService);
        }

        public Task<Models.Player> GetPlayerById(int playerId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlayersEndpoint);
            builder.AppendToPath(playerId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<Models.Player>(uri);
        }

        public Task<IEnumerable<Models.Player>> GetPlayerByName(string name)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlayersEndpoint);
            builder.AppendToPath($"name/{name}");
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Player>>(uri);
        }

        public Task<IEnumerable<Models.Team>> GetPlayerTeams(int playerId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlayersEndpoint);
            builder.AppendToPath($"Teams/{playerId}");
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Team>>(uri);
        }

        public Task<IEnumerable<Models.Skill>> GetSkills(int playerId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlayersEndpoint);
            builder.AppendToPath($"Skills/{playerId}");
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Skill>>(uri);
        }

        public Task<IEnumerable<Models.Contact>> GetPlayerContacts(int playerId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlayersEndpoint);
            builder.AppendToPath("Contacts");
            builder.AppendToPath(playerId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Contact>>(uri);
        }

        public async Task<bool> PostPlayerSkill(PlayerSkill playerSkill)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlayersEndpoint);
            builder.AppendToPath("Skill");
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.PostAsync<Models.PlayerSkill, bool>(uri, playerSkill, token);
        }

        public async Task<Models.Contact> PostPlayerContact(int playerId, Models.Contact contact)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlayersEndpoint);
            builder.AppendToPath("Contact");
            builder.AppendToPath(playerId.ToString());
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.PostAsync<Models.Contact, Models.Contact>(uri, contact, token);
        }

        public Task<Models.Player> PostPlayerAsync(Models.Player player)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlayersEndpoint);
            string uri = builder.ToString();

            return _requestService.PostAsync<Models.Player, Models.Player>(uri, player);
        }

        public async Task<bool> DeletePlayerAsync(int playerId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlayersEndpoint);
            builder.AppendToPath(playerId.ToString());
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.DeleteAsync(uri, token);
        }
    }
}
