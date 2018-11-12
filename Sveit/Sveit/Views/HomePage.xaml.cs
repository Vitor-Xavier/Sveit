using Sveit.Services.Requests;
using Sveit.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage(IRequestService requestService)
        {
            InitializeComponent();
            BindingContext = new HomeViewModel(requestService);
        }
    }
}