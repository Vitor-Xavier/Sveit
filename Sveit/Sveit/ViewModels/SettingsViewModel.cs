using Plugin.Multilingual;
using Sveit.Services.Requests;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    class SettingsViewModel : BaseViewModel
    {
        public ICommand TwitterCommand => new Command(TwitterCommandExecute);

        public ICommand FacebookCommand => new Command(FacebookCommandExecute);

        public ICommand LinkedInCommand => new Command(LinkedInCommandExecute);

        public ICommand GoogleCommand => new Command(GoogleCommandExecute);

        public ICommand GithubCommand => new Command(GithubCommandExecute);

        public ObservableCollection<string> Languages { get; set; }

        private string _language;

        public string Language
        {
            get { return _language; }
            set { _language = value; ChangeLanguage(_language); OnPropertyChanged(); }
        }

        public SettingsViewModel(IRequestService requestService)
        {
            Languages = new ObservableCollection<string>
            {
                "English",
                "Português"
            };
            Language = AppResources.Culture.IetfLanguageTag.Equals("en-US") ? "English" : "Português";
        }

        private void ChangeLanguage(string language)
        {
            if (language.Equals("English") && !AppResources.Culture.IetfLanguageTag.Equals("en-US"))
            {
                CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo("en-US");
                AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            }
            else if (language.Equals("Português") && !AppResources.Culture.IetfLanguageTag.Equals("pt-BR"))
            {
                CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo("pt-BR");
                AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            }
        }

        private void TwitterCommandExecute()
        {
            try
            {
                Device.OpenUri(new Uri("twitter://user?user_id=881263290575450112"));
            }
            catch (Exception)
            {
                Device.OpenUri(new Uri("https://twitter.com/vitorvxs"));
            }
        }

        private void FacebookCommandExecute()
        {
            try
            {
                Device.OpenUri(new Uri("fb://page/100005920290382"));
            }
            catch (Exception)
            {
                Device.OpenUri(new Uri("https://www.facebook.com/vitor.xavier.167"));
            }
        }

        private void LinkedInCommandExecute()
        {
            try
            {
                Device.OpenUri(new Uri("https://linkedin.com/in/vitor-xavier"));
            }
            catch (Exception) { }
        }

        private void GoogleCommandExecute()
        {
            Device.OpenUri(new Uri("https://plus.google.com/100082514671815234409"));
        }

        private void GithubCommandExecute()
        {
            Device.OpenUri(new Uri("https://github.com/Vitor-Xavier"));
        }
    }
}
