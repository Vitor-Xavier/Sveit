using Sveit.Services.Requests;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApplyRegisterPage : ContentPage
    {
        public ApplyRegisterPage(IRequestService requestService, Models.Vacancy vacancy)
        {
            InitializeComponent();
            BindingContext = new ViewModels.ApplyRegisterViewModel(Navigation, requestService, vacancy);
        }
    }
}