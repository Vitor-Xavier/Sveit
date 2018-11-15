using Sveit.Models;
using Sveit.Services.Requests;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommentRegisterPage : ContentPage
    {
        public CommentRegisterPage(IRequestService requestService, Player player)
        {
            InitializeComponent();
            BindingContext = new ViewModels.CommentRegisterViewModel(Navigation, requestService, player);
        }
    }
}