using Sveit.Services.Login;
using Sveit.Services.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Sveit.Views.MasterMainPageMaster;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMainPage : MasterDetailPage
    {
        private ILoginService _loginService;

        public MasterMainPage()
        {
            InitializeComponent();
            if (AppSettings.ApiStatus)
                _loginService = new LoginService(new RequestService());
            else
                _loginService = new FakeLoginService();
            
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterMenuItem;
            if (item == null)
                return;

            if (item.Title == "Exit")
            {
                
                if (_loginService.LogOut())
                    App.Current.MainPage = new Sveit.Views.MasterMainPage();
            }

            var page = (Page)Activator.CreateInstance(item.TargetType);
            //page.Title = item.Title;
            
            Detail = new NavigationPage(page)
            {
                BarBackgroundColor = item.TransparentNavBar ? Color.Transparent : (Color)Application.Current.Resources["AccentColor"],
                BarTextColor = Color.FromHex("#FFFFFF"),
            };
            
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}