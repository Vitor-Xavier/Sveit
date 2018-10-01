using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VacanciesPage : ContentPage
    {
        public VacanciesPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.VacanciesViewModel();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            await Navigation.PushAsync(new VacancyPage());

            ((ListView)sender).SelectedItem = null;
        }
    }
}