using Sveit.Services.Requests;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsTeamRegisterPage : ContentPage
    {
        public ContactsTeamRegisterPage(IRequestService requestService, Models.Team team)
        {
            InitializeComponent();
            BindingContext = new ViewModels.ContactsTeamRegisterViewModel(Navigation, requestService, team);
        }
    }
}