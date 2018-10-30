
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApplyPage : ContentPage
    {
        public ApplyPage(Models.Apply apply, bool isEvaluation = false)
        {
            InitializeComponent();
            BindingContext = new ViewModels.ApplyViewModel(Navigation, apply, isEvaluation);
        }
    }
}