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
            BindingContext = new ViewModels.VacanciesViewModel(Navigation);
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = ((ListView)sender).SelectedItem as Models.Vacancy;
            if (item == null) return;
            (BindingContext as ViewModels.VacanciesViewModel).VacancySelectedCommandExecute(item);
            ((ListView)sender).SelectedItem = null;
        }
    }
}