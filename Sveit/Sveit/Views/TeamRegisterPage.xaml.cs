using Sveit.Services.Requests;
using Sveit.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamRegisterPage : ContentPage
    {
        public TeamRegisterPage(IRequestService requestService, int teamId = 0)
        {
            InitializeComponent();
            BindingContext = new TeamRegisterViewModel(Navigation, requestService, teamId);
        }
    }
}