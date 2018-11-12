
using Sveit.Models;
using Sveit.Services.Login;
using Sveit.Services.Requests;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMainPageMaster : ContentPage
    {
        public ListView ListView;

        public MasterMainPageMaster(IRequestService requestService)
        {
            InitializeComponent();

            BindingContext = new ViewModels.MasterMainViewModel(requestService);
            ListView = MenuItemsListView;
        }
    }
}