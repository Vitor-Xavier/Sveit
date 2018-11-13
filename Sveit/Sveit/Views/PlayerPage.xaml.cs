using Sveit.Services.Requests;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerPage : ContentPage
    {
        public PlayerPage(IRequestService requestService, int playerId)
        {
            InitializeComponent();
            BindingContext = new ViewModels.PlayerViewModel(Navigation, requestService, playerId);
        }

        public PlayerPage(IRequestService requestService)
        {
            InitializeComponent();
            BindingContext = new ViewModels.PlayerViewModel(Navigation, requestService);
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = ((ListView)sender).SelectedItem as Models.Team;
            if (item == null) return;
            await (BindingContext as ViewModels.PlayerViewModel).TeamSelectedCommandExecute(item);
            ((ListView)sender).SelectedItem = null;
        }
    }
}