using Sveit.Services.Requests;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VacanciesTeamPage : ContentPage
    {
        public VacanciesTeamPage(IRequestService requestService, int teamId)
        {
            InitializeComponent();
            BindingContext = new ViewModels.VacanciesTeamViewModel(Navigation, requestService, teamId);
        }
    }
}