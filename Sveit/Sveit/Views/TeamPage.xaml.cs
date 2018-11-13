using Sveit.Models;
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
	public partial class TeamPage : ContentPage
	{
		public TeamPage (IRequestService requestService, int teamId)
		{
			InitializeComponent ();
            BindingContext = new ViewModels.TeamViewModel(Navigation, requestService, teamId);
		}
	}
}