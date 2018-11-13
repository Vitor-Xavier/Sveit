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
	public partial class VacancyRegisterPage : ContentPage
	{
		public VacancyRegisterPage (IRequestService requestService, Team team)
		{
			InitializeComponent ();
            BindingContext = new ViewModels.VacancyRegisterViewModel(Navigation, requestService, team);
		}
	}
}