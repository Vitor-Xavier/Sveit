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
        public GamesPage()
        {
            InitializeComponent();
            BindingContext = new GamesViewModel(Navigation);
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = ((ListView)sender).SelectedItem as Models.Game;
            if (item == null) return;
            (BindingContext as ViewModels.GamesViewModel).GameSelectedCommandExecute(item);
            ((ListView)sender).SelectedItem = null;
        }
    }
}