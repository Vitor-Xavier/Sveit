using Sveit.Services.Requests;
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

            if (storagedEmail != null && storagedPassword != null && storagedToken != null)
            {
                var player = new Models.Player { };
                App.LoggedPlayer = player;
                return player;
            }
            else
                return null;
        }

        public async Task<Models.Player> LogIn(string email, string password)
        {
            var player = new Models.Player { };
            var oauthToken = "key";
            try
            {
                await SecureStorage.SetAsync("Sveit-Email", email);
                await SecureStorage.SetAsync("Sveit-Password", password);
                await SecureStorage.SetAsync("Sveit-OAuthToken", oauthToken);
                return player;
            }
            catch
            {
                //TODO: LogIn from server
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
