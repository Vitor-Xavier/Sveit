using Sveit.Services.Login;
using Sveit.Services.Requests;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class LoginViewModel : BindableObject
    {
        private readonly ILoginService _loginService;

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

        public ICommand LogInCommand => new Command(LogInCommandExecute);

        public LoginViewModel(IRequestService requestService)
        {
            _loginService = new LoginService(requestService);
        }

        private void SignUpCommandExecute()
        {
            App.Current.MainPage = new Views.PlayerRegisterPage();
        }

        private async void LogInCommandExecute()
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
                await App.Current.MainPage.DisplayAlert("Sveit", "Logged", "Ok");
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

        static string ComputeSha256Hash(string rawData)
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
