using Sveit.Services.Requests;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VacanciesPage : ContentPage
    {
        public VacanciesPage(IRequestService requestService)
        {
            InitializeComponent();
            BindingContext = new ViewModels.VacanciesViewModel(Navigation, requestService);
        }
    }
}