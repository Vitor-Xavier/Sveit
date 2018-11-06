using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sveit.Extensions;
using Sveit.Models;
using Sveit.Services.Login;
using Sveit.Services.Requests;

namespace Sveit.Services.Genre
{
    class GenreService : IGenreService
    {
        private readonly IRequestService _requestService;

        private readonly ILoginService _loginService;

        public GenreService(IRequestService requestService)
        {
            _requestService = requestService;
            _loginService = new LoginService(_requestService);
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

        public async Task<bool> AddGenreAsync(Models.Genre genre)
        {
            string uri = AppSettings.GenresEndpoint;

            var token = await _loginService.GetOAuthToken();
            return await _requestService.PostAsync<Models.Genre, bool>(uri, genre, token);
        }

        public async Task<bool> RemoveGenreAsync(int genreId)
        {
            UriBuilder builder = new UriBuilder(AppSettings.GenresEndpoint);
            builder.AppendToPath(genreId.ToString());
            string uri = builder.ToString();

            var token = await _loginService.GetOAuthToken();
            return await _requestService.DeleteAsync(uri, token);
        }
    }
}
