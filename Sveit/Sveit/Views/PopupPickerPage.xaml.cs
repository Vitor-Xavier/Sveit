﻿using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
	public partial class PopupPickerPage : PopupPage
	{
		public PopupPickerPage ()
		{
			InitializeComponent ();
		}

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopAllPopupAsync();
            return base.OnBackButtonPressed();
        }
    }
}