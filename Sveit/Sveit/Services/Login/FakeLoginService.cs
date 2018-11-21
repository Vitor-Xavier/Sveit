using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Sveit.Services.Login
{
    public class FakeLoginService : ILoginService
    {
        public async Task<Models.Player> CheckLogIn()
        {
            var storagedUsername = await SecureStorage.GetAsync("Sveit-Username");
            var storagedPassword = await SecureStorage.GetAsync("Sveit-Password");

            if (storagedUsername != null && storagedPassword != null)
            {
                var player = new Models.Player
                {
                    PlayerId = 1,
                    AvatarSource = "https://i.pinimg.com/originals/c8/0a/09/c80a0933df51f9f5be92d033c6db65b2.jpg",
                    Nickname = "vitorxs",
                    Name = "Vitor Xavier de Souza",
                    Gender = new Models.Gender { Name = "Masculino" },
                    DateOfBirth = new System.DateTime(1997, 01, 06)
                };
                App.LoggedPlayer = player;
                return player;
            }
            else
                return null;
        }

        public Task<string> GetOAuthToken()
        {
            return Task.FromResult("key");
        }

        public async Task<Models.Player> LogIn(string username, string password)
        {
            if (username.Equals("admin") &&
                password.Equals("8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918"))
            {
                var oauthToken = "key";
                var player = new Models.Player
                {
                    PlayerId = 1,
                    AvatarSource = "https://i.pinimg.com/originals/c8/0a/09/c80a0933df51f9f5be92d033c6db65b2.jpg",
                    Nickname = "Vitorxs",
                    Name = "Vitor Xavier de Souza",
                    Gender = new Models.Gender { Name = "Masculino" },
                    DateOfBirth = new System.DateTime(1997, 01, 06)
                };
                
                await SecureStorage.SetAsync("Sveit-Username", username);
                await SecureStorage.SetAsync("Sveit-Password", password);
                await SecureStorage.SetAsync("Sveit-OAuthToken", oauthToken);
                await SecureStorage.SetAsync("Sveit-DateTime", DateTime.Now.ToString());

                App.LoggedPlayer = player;
                return player;
            }
            else
                return null;
        }

        public bool LogOut()
        {
            try
            {
                var username = SecureStorage.Remove("Sveit-Username");
                var password = SecureStorage.Remove("Sveit-Password");
                var oauthToken = SecureStorage.Remove("Sveit-OAuthToken");
                var tokenTime = SecureStorage.Remove("Sveit-DateTime");
                App.LoggedPlayer = null;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
