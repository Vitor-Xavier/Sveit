using Plugin.Multilingual;
using Sveit.Services.Requests;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IRequestService _requestService;

        private bool storeCredentials;

        public bool StoreCredentials
        {
            get { return storeCredentials; }
            set
            {
                storeCredentials = value;
                OnPropertyChanged();
                ChangeStoreCredentialsStatus(value);
            }
        }

        public ICommand TwitterCommand => new Command(TwitterCommandExecute);

        public ICommand FacebookCommand => new Command(FacebookCommandExecute);

        public ICommand LinkedInCommand => new Command(LinkedInCommandExecute);

        public ICommand OutlookCommand => new Command(OutlookCommandExecute);

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
            _requestService = requestService;
            Languages = new ObservableCollection<string>
            {
                "English",
                "Português"
            };
            Language = AppResources.Culture.IetfLanguageTag.Equals("en-US") ? "English" : "Português";
            StoreCredentials = AppSettings.CredentialStatus;
        }

        private void ChangeLanguage(string language)
        {
            if (language.Equals("English") && !AppResources.Culture.IetfLanguageTag.Equals("en-US"))
            {
                CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo("en-US");
                AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
                AppSettings.Language = CrossMultilingual.Current.CurrentCultureInfo.IetfLanguageTag;
                App.Current.MainPage = new Views.MasterMainPage(_requestService);
            }
            else if (language.Equals("Português") && !AppResources.Culture.IetfLanguageTag.Equals("pt-BR"))
            {
                CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo("pt-BR");
                AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;
                AppSettings.Language = CrossMultilingual.Current.CurrentCultureInfo.IetfLanguageTag;
                App.Current.MainPage = new Views.MasterMainPage(_requestService);
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

        private void OutlookCommandExecute()
        {
            Device.OpenUri(new Uri("mailto:vitorvxs@live.com"));
        }

        private void GithubCommandExecute()
        {
            Device.OpenUri(new Uri("https://github.com/Vitor-Xavier"));
        }

        private void ChangeStoreCredentialsStatus(bool value)
        {
            AppSettings.CredentialStatus = value;
        }
    }
}
