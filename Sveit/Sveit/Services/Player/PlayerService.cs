﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Requests;

namespace Sveit.Services.Player
{
    class PlayerService : IPlayerService
    {
        private readonly IRequestService _requestService;

        public PlayerService(IRequestService requestService)
        {
            _requestService = requestService;
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

        public Task<bool> PostPlayerSkill(PlayerSkill playerSkill)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlayersEndpoint);
            builder.AppendToPath("Skill");
            string uri = builder.ToString();

            return _requestService.PostAsync<Models.PlayerSkill, bool>(uri, playerSkill);
        }

        public Task<Models.Contact> PostPlayerContact(int playerId, Models.Contact contact)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlayersEndpoint);
            builder.AppendToPath("Contact");
            builder.AppendToPath(playerId.ToString());
            string uri = builder.ToString();

            return _requestService.PostAsync<Models.Contact, Models.Contact>(uri, contact);
        }

        public Task<Models.Player> PostPlayerAsync(Models.Player player)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlayersEndpoint);
            string uri = builder.ToString();

            return _requestService.PostAsync<Models.Player, Models.Player>(uri, player);
        }

        public Task<bool> DeletePlayerAsync(int playerId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlayersEndpoint);
            builder.AppendToPath(playerId.ToString());
            string uri = builder.ToString();

            return _requestService.DeleteAsync(uri, "");
        }
    }
}
