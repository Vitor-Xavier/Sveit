using Sveit.Controls;
using Sveit.Extensions;
using Sveit.Services.Login;
using Sveit.Services.Requests;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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

        public ICommand SignUpCommand => new Command(SignUpCommandExecute);

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

        private void SignUpCommandExecute()
        {
            _navigation.PushModalAsync(new Views.PlayerRegisterPage(_requestService));
        }

        private async Task LogInCommandExecute()
        {
            if (!Validate()) return;
            var pass = Utils.SHA2Utilities.ComputeSha256Hash(Password);

            var player = await _loginService.LogIn(Email, pass);
            if (player == null)
            {
                DependencyService.Get<IMessage>().ShortAlert(AppResources.LoginFailed);
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

    }
}
