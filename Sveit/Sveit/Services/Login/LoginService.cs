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

        private readonly IPlayerService _playerService;

        public LoginService(IRequestService requestService)
        {
            _requestService = requestService;
            _playerService = new PlayerService(_requestService);
        }

        public async Task<Models.Player> CheckLogIn()
        {
            var storagedEmail = await SecureStorage.GetAsync("Sveit-Email");
            var storagedPassword = await SecureStorage.GetAsync("Sveit-Password");
            var storagedToken = await SecureStorage.GetAsync("Sveit-OAuthToken");
            var storagedTokenTime = await SecureStorage.GetAsync("Sveit-DateTime");

            string tokenRequest = $"username={storagedEmail}&password={storagedPassword}&grant_type=password";
            string oauthToken = await _requestService.PostRawAsync(AppSettings.TokenEndpoint, tokenRequest);
            if (!string.IsNullOrWhiteSpace(oauthToken))
            {
                var player = await _playerService.GetByEmail(storagedEmail);
                App.LoggedPlayer = player;
                return player;
            }
            else
                return null;
        }

        public async Task<Models.Player> LogIn(string email, string password)
        {
            string tokenRequest = $"username=[{email}]&password=[{password}]&grant_type=password";
            string oauthToken = await _requestService.PostRawAsync(AppSettings.TokenEndpoint, tokenRequest);
            var player = await _playerService.GetByEmail(email);
            try
            {
                await SecureStorage.SetAsync("Sveit-Email", email);
                await SecureStorage.SetAsync("Sveit-Password", password);
                await SecureStorage.SetAsync("Sveit-OAuthToken", oauthToken);
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
    }
}
