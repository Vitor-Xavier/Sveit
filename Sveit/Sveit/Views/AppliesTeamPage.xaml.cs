using Sveit.Services.Requests;
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
		public AppliesTeamPage (IRequestService requestService, Models.Vacancy vacancy)
		{
			InitializeComponent ();
            BindingContext = new ViewModels.AppliesTeamViewModel(Navigation, requestService, vacancy);

        }
    }
}