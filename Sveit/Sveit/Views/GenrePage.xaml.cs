using Sveit.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenrePage : ContentPage
    {
        public GenrePage()
        {
            InitializeComponent();
            BindingContext = new GenreViewModel();
        }
    }
}