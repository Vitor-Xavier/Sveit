using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.Multilingual;
using Sveit.Models;
using Sveit.Services.Requests;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Sveit
{
    public partial class App : Application
    {
        public static Player LoggedPlayer { get; set; }

        public App()
        {
            InitializeComponent();
            
            AppResources.Culture = CrossMultilingual.Current.DeviceCultureInfo;

            MainPage = new Sveit.Views.MasterMainPage();
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=a480444b-18dc-480b-82a7-1dcb3947635b;" +
                //"uwp={Your UWP App secret here};" +
                "ios=b46a867a-2620-4ec3-835b-da686fa228ab",
                typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
