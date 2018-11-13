using Sveit.Extensions;
using Sveit.Services.Login;
using Sveit.Services.Requests;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        private readonly ILoginService _loginService;

        private readonly IRequestService _requestService;

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }

        public IAsyncCommand SignUpCommand => new AsyncCommand(SignUpCommandExecute);

        public IAsyncCommand LogInCommand => new AsyncCommand(LogInCommandExecute);

        public LoginViewModel(INavigation navigation, IRequestService requestService)
        {
            _navigation = navigation;
            _requestService = requestService;
            if (AppSettings.ApiStatus)
                _loginService = new LoginService(requestService);
            else
                _loginService = new FakeLoginService();
        }

        private async Task SignUpCommandExecute()
        {
            await _navigation.PushModalAsync(new Views.PlayerRegisterPage());
        }

        private async Task LogInCommandExecute()
        {
            if (!Validate()) return;
            var pass = ComputeSha256Hash(Password);

            var player = await _loginService.LogIn(Email, pass);
            if (player == null)
            {
                return;
            }
            else
            {
                App.Current.MainPage = new Views.MasterMainPage(_requestService);
            }
        }

        private bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Email))
                return false;
            if (Password.Length <= 4 || Email.Length <= 4)
                return false;
            return true;
        }

        private static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
