using Sveit.Services.Requests;
using Sveit.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamesPage : ContentPage
    {
        public GamesPage(IRequestService requestService)
        {
            InitializeComponent();
            BindingContext = new GamesViewModel(Navigation, requestService);
        }
    }
}