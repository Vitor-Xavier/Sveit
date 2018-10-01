using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMainPage : MasterDetailPage
    {
        public MasterMainPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterMenuItem;
            if (item == null)
                return;

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