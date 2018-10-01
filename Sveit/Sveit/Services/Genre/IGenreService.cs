using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.Services.Genre
{
    interface IGenreService
    {
        Task<Models.Genre> GetGenreAsync(int genreId);

        Task<IEnumerable<Models.Genre>> GetGenresAsync();

        Task<IEnumerable<Models.Genre>> GetGenresByNameAsync(string name);

        Task<bool> AddGenreAsync(Models.Genre genre);

        Task<bool> RemoveGenreAsync(int genreId);
    }
}
