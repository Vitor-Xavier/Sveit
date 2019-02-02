using Sveit.Controls;
using Sveit.Services.Login;
using Sveit.Services.Requests;
using Sveit.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Sveit.Views.MasterMainPageMaster;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailMainPage : MasterDetailPage
    {
        public MasterDetailMainPage()
        {
            InitializeComponent();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(e.SelectedItem is MasterMenuItem item))
                return;
            await (BindingContext as MasterMainViewModel).ItemSelected(item);
        }
    }
}