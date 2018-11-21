using Sveit.Services.Requests;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerRegisterPage : ContentPage
    {
        public PlayerRegisterPage(IRequestService requestService, int playerId = 0)
        {
            InitializeComponent();
            BindingContext = new ViewModels.PlayerRegisterViewModel(Navigation, requestService, playerId);

        }
    }
}