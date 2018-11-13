using Sveit.Controls;
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

        private IRequestService _requestService;

        public MasterMainPage(IRequestService requestService)
        {
            InitializeComponent();
            _requestService = requestService;
            if (AppSettings.ApiStatus)
                _loginService = new LoginService(requestService);
            else
                _loginService = new FakeLoginService();
            Master = new MasterMainPageMaster(_requestService);
            Detail = new NavigationPage(new HomePage(_requestService))
            {
                BarBackgroundColor = (Color)App.Current.Resources["AccentColor"],
                BarTextColor = (Color)App.Current.Resources["PrimaryText"]
            };
            (Master as MasterMainPageMaster).ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterMenuItem;
            if (item == null)
                return;

            if (item.TargetType == null)
            {
                
                if (_loginService.LogOut())
                {
                    App.Current.MainPage = new Sveit.Views.MasterMainPage(_requestService);
                    return;
                }

            }

            var page = (Page)Activator.CreateInstance(item.TargetType, _requestService);
            //page.Title = item.Title;
            
            if (item.TransparentNavBar)
            {
                Detail = new CustomNavigationPage(page);
            }
            else
            {
                Detail = new NavigationPage(page)
                {
                    BarBackgroundColor = item.TransparentNavBar ? Color.Transparent : (Color)Application.Current.Resources["AccentColor"],
                    BarTextColor = Color.FromHex("#FFFFFF"),
                };
            }
            
            IsPresented = false;

            (Master as MasterMainPageMaster).ListView.SelectedItem = null;
        }
    }
}