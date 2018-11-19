using Sveit.Extensions;
using Sveit.Services.Requests;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Sveit.Services.Login
{
    public class LoginService : ILoginService
    {
        private readonly IRequestService _requestService;

        public LoginService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task<Models.Player> CheckLogIn()
        {
            var storagedUsername = await SecureStorage.GetAsync("Sveit-Username");
            var storagedPassword = await SecureStorage.GetAsync("Sveit-Password");
            var storagedToken = await SecureStorage.GetAsync("Sveit-OAuthToken");
            var storagedTokenTime = await SecureStorage.GetAsync("Sveit-DateTime");

            string tokenRequest = $"username={storagedUsername}&password={storagedPassword}&grant_type=password";
            Token oauthToken = await _requestService.PostRawAsync<Token>(AppSettings.AuthEndpoint, tokenRequest);
            if (oauthToken != null)
            {
                var player = await GetByUsername(storagedUsername);
                App.LoggedPlayer = player;
                return player;
            }
            else
                return null;
        }

        public async Task<Models.Player> LogIn(string username, string password)
        {
            string tokenRequest = $"username={username}&password={password}&grant_type=password";
            try
            {
                Token oauthToken = await _requestService.PostRawAsync<Token>(AppSettings.AuthEndpoint, tokenRequest);

                if (oauthToken == null) return null;
                
                await SecureStorage.SetAsync("Sveit-Username", username);
                await SecureStorage.SetAsync("Sveit-Password", password);
                await SecureStorage.SetAsync("Sveit-OAuthToken", oauthToken.Access_token);
                await SecureStorage.SetAsync("Sveit-DateTime", DateTime.Now.ToString());
                var player = await GetByUsername(username);
                return player;
            }
            catch
            {
                return null;
            }
        }

        public bool LogOut()
        {
            try
            {
                var email = SecureStorage.Remove("Sveit-Username");
                var password = SecureStorage.Remove("Sveit-Password");
                var oauthToken = SecureStorage.Remove("Sveit-OAuthToken");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetOAuthToken()
        {
            var storagedTokenTime = await SecureStorage.GetAsync("Sveit-DateTime");
            if (DateTime.TryParse(storagedTokenTime, out DateTime tokenTime))
            {
                TimeSpan ts = DateTime.Now.Subtract(tokenTime);

                if (ts.TotalMinutes <= AppSettings.TokenDuration)
                    return await SecureStorage.GetAsync("Sveit-OAuthToken");
            }
            var storagedUsername = await SecureStorage.GetAsync("Sveit-Username");
            var storagedPassword = await SecureStorage.GetAsync("Sveit-Password");

            string tokenRequest = $"username={storagedUsername}&password={storagedPassword}&grant_type=password";
            Token oauthToken = await _requestService.PostRawAsync<Token>(AppSettings.AuthEndpoint, tokenRequest);

            if (oauthToken == null) return null;

            await SecureStorage.SetAsync("Sveit-OAuthToken", oauthToken.Access_token);
            await SecureStorage.SetAsync("Sveit-DateTime", DateTime.Now.ToString());

            return oauthToken.Access_token;
        }

        public Task<Models.Player> GetByUsername(string username)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlayersEndpoint);
            builder.AppendToPath("Username");
            builder.AppendToPath(username);
            string uri = builder.ToString();

            return _requestService.GetAsync<Models.Player>(uri);
        }
    }
}
