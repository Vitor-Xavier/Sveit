using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sveit.Services.Game
{
    public interface IGameService
    {
        Task<Models.Game> GetGameAsync(int gameId);

        Task<IEnumerable<Models.Game>> GetGamesAsync();

        Task<IEnumerable<Models.Game>> GetTrendGamesAsync();

        Task<IEnumerable<Models.Game>> GetGamesAsync(string name);

        Task<IEnumerable<Models.Game>> GetGamesByPlatformAsync(int platformId);

        Task<bool> PostGameAsync(Models.Game game);

        Task<bool> DeleteGameAsync(int gameId);

        
    }
}
