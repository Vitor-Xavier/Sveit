using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Requests;

namespace Sveit.Services.Genre
{
    class GenreService : IGenreService
    {
        private readonly IRequestService _requestService;

        public GenreService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<Models.Genre> GetGenreAsync(int genreId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.GenresEndpoint);
            builder.AppendToPath(genreId.ToString());
            string uri = builder.ToString();

            return _requestService.GetAsync<Models.Genre>(uri);
        }

        public Task<IEnumerable<Models.Genre>> GetGenresAsync()
        {
            string uri = AppSettings.GenresEndpoint;

            return _requestService.GetAsync<IEnumerable<Models.Genre>>(uri);
        }

        public Task<IEnumerable<Models.Genre>> GetGenresByNameAsync(string name)
        {
            UriBuilder builder = new UriBuilder(AppSettings.GenresEndpoint);
            builder.AppendToPath("Name");
            builder.AppendToPath(name);
            string uri = builder.ToString();

            return _requestService.GetAsync<IEnumerable<Models.Genre>>(uri);
        }

        public Task<bool> AddGenreAsync(Models.Genre genre)
        {
            string uri = AppSettings.GenresEndpoint;

            return _requestService.PostAsync<Models.Genre, bool>(uri, genre);
        }

        public Task<bool> RemoveGenreAsync(int genreId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.GenresEndpoint);
            builder.AppendToPath(genreId.ToString());
            string uri = builder.ToString();

            return _requestService.DeleteAsync(uri, "");
        }
    }
}
