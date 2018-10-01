using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Requests;

namespace Sveit.Services.Game
{
    class GameService : IGameService
    {
        private readonly IRequestService _requestService;

        public GameService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<Models.Game> GetGameAsync(int gameId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.GamesEndpoint);
            builder.AppendToPath(gameId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<Models.Game>(uri);
        }

        public Task<IEnumerable<Models.Game>> GetGamesAsync()
        {
            string uri = AppSettings.GamesEndpoint;

            return _requestService.GetAsync<IEnumerable<Models.Game>>(uri);
        }
        
        public Task<IEnumerable<Models.Game>> GetGamesAsync(string name)
        {
            UriBuilder builder = new UriBuilder(AppSettings.GamesEndpoint);
            builder.AppendToPath($"Name/{name}");
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Game>>(uri);
        }

        public Task<IEnumerable<Models.Game>> GetTrendGamesAsync()
        {
            string uri = AppSettings.GamesEndpoint;

            return _requestService.GetAsync<IEnumerable<Models.Game>>(uri);
        }

        public Task<IEnumerable<Models.Game>> GetGamesByPlatformAsync(int platformId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.GamesEndpoint);
            builder.AppendToPath($"Platform/{platformId}");
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Game>>(uri);
        }

        public Task<bool> PostGameAsync(Models.Game game)
        {
            string uri = AppSettings.GamesEndpoint;

            return _requestService.PostAsync<Models.Game, bool>(uri, game);
        }

        public Task<bool> DeleteGameAsync(int gameId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.GamesEndpoint);
            builder.AppendToPath(gameId.ToString());
            string uri = builder.ToString();

            return _requestService.DeleteAsync(uri, "");
        }
    }
}