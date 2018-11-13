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

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = ((ListView)sender).SelectedItem as Models.Vacancy;
            if (item == null) return;
            await (BindingContext as ViewModels.VacanciesViewModel).VacancyCommandExecute(item);
            ((ListView)sender).SelectedItem = null;
        }
    }
}