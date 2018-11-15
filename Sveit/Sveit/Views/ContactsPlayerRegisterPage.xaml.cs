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
	public partial class ContactsPlayerRegisterPage : ContentPage
	{
		public ContactsPlayerRegisterPage (IRequestService requestService, Player player)
		{
			InitializeComponent ();
            BindingContext = new ViewModels.ContactsPlayerRegisterViewModel(Navigation, requestService, player);
		}
	}
}