﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VacanciesTeamPage : ContentPage
	{
		public VacanciesTeamPage (int teamId)
		{
			InitializeComponent ();
            BindingContext = new ViewModels.VacanciesTeamViewModel(Navigation, teamId);
		}

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = ((ListView)sender).SelectedItem as Models.Vacancy;
            if (item == null) return;
            (BindingContext as ViewModels.VacanciesTeamViewModel).VacancySelectCommandExecute(item);
            ((ListView)sender).SelectedItem = null;
        }
    }
}