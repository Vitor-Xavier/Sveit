using Newtonsoft.Json.Linq;
using Sveit.Extensions;
using Sveit.Services.Player;
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
            var storagedEmail = await SecureStorage.GetAsync("Sveit-Email");
            var storagedPassword = await SecureStorage.GetAsync("Sveit-Password");
            var storagedToken = await SecureStorage.GetAsync("Sveit-OAuthToken");
            var storagedTokenTime = await SecureStorage.GetAsync("Sveit-DateTime");

            string tokenRequest = $"username={storagedEmail}&password={storagedPassword}&grant_type=password";
            Token oauthToken = await _requestService.PostRawAsync<Token>(AppSettings.TokenEndpoint, tokenRequest);
            if (oauthToken != null)
            {
                var player = await GetByEmail(storagedEmail);
                App.LoggedPlayer = player;
                return player;
            }
            else
                return null;
        }

        public async Task<Models.Player> LogIn(string email, string password)
        {
            string tokenRequest = $"username={email}&password={password}&grant_type=password";
            try
            {
                Token oauthToken = await _requestService.PostRawAsync<Token>(AppSettings.TokenEndpoint, tokenRequest);

                if (oauthToken == null) return null;
                var player = await GetByEmail(email);
                await SecureStorage.SetAsync("Sveit-Email", email);
                await SecureStorage.SetAsync("Sveit-Password", password);
                await SecureStorage.SetAsync("Sveit-OAuthToken", oauthToken.Access_token);
                await SecureStorage.SetAsync("Sveit-DateTime", DateTime.Now.ToString());
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
                var email = SecureStorage.Remove("Sveit-Email");
                var password = SecureStorage.Remove("Sveit-Password");
                var oauthToken = SecureStorage.Remove("Sveit-OAuthToken");
                return email && password && oauthToken;
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

                if (ts.TotalMinutes <= 90.0)
                    return await SecureStorage.GetAsync("Sveit-OAuthToken");
            }
            var storagedEmail = await SecureStorage.GetAsync("Sveit-Email");
            var storagedPassword = await SecureStorage.GetAsync("Sveit-Password");

            string tokenRequest = $"username=[{storagedEmail}]&password=[{storagedPassword}]&grant_type=password";
            Token oauthToken = await _requestService.PostRawAsync<Token>(AppSettings.TokenEndpoint, tokenRequest);

            if (oauthToken == null) return null;

            await SecureStorage.SetAsync("Sveit-OAuthToken", oauthToken.Access_token);
            await SecureStorage.SetAsync("Sveit-DateTime", DateTime.Now.ToString());

            return oauthToken.Access_token;
        }

        public Task<Models.Player> GetByEmail(string email)
        {
            UriBuilder builder = new UriBuilder(AppSettings.PlayersEndpoint);
            builder.AppendToPath("Email");
            builder.AppendToPath(email);
            string uri = builder.ToString();

            return _requestService.GetAsync<Models.Player>(uri);
        }
    }
}
