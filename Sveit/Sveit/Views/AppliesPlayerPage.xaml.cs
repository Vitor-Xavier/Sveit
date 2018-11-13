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
    public partial class AppliesPlayerPage : ContentPage
    {
        public AppliesPlayerPage(int playerId)
        {
            InitializeComponent();
            BindingContext = new ViewModels.AppliesPlayerViewModel(Navigation, playerId);
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = ((ListView)sender).SelectedItem as Models.Apply;
            if (item == null) return;
            await (BindingContext as ViewModels.AppliesPlayerViewModel).ApplySelectedCommandExecute(item);
            ((ListView)sender).SelectedItem = null;
        }

    }
}