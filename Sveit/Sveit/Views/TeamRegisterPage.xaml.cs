using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sveit.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TeamRegisterPage : ContentPage
	{
		public TeamRegisterPage ()
		{
			InitializeComponent ();
            BindingContext = new TeamRegisterViewModel(Navigation);
		}
	}
}