using Plugin.Multilingual;
using Sveit.Services.Requests;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage(IRequestService requestService)
        {
            InitializeComponent();
            BindingContext = new ViewModels.SettingsViewModel(requestService);
        }
    }
}