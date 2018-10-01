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

        public Task<bool> LogOut()
        {
            try
            {
                var email = SecureStorage.Remove("Sveit-Email");
                var password = SecureStorage.Remove("Sveit-Password");
                var oauthToken = SecureStorage.Remove("Sveit-OAuthToken");
                return Task.FromResult(email && password && oauthToken);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
    }
}
