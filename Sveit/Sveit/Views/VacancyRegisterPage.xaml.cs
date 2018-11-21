using Sveit.Models;
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
	public partial class VacancyRegisterPage : ContentPage
    {
		public VacancyRegisterPage (IRequestService requestService, Team team, int vacancyId = 0)
		{
            Resources = new ResourceDictionary();
            Resources.Add("SkillValidatorFactory", new Func<string, object>(
                (arg) => (BindingContext as VacancyRegisterViewModel)?.ValidateAndReturn(arg)));

            InitializeComponent ();
            BindingContext = new VacancyRegisterViewModel(Navigation, requestService, team, vacancyId);
		}
    }
}