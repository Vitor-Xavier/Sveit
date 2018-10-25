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
	public partial class AppliesTeamPage : ContentPage
	{
		public AppliesTeamPage (Models.Vacancy vacancy)
		{
			InitializeComponent ();
            BindingContext = new ViewModels.AppliesTeamViewModel(Navigation, vacancy);

        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = ((ListView)sender).SelectedItem as Models.Apply;
            if (item == null) return;
            (BindingContext as ViewModels.AppliesTeamViewModel).ApplySelectedCommandExecute(item);
            ((ListView)sender).SelectedItem = null;
        }
    }
}