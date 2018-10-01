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
	public partial class PlayerRegisterPage : ContentPage
	{
		public PlayerRegisterPage ()
		{
			InitializeComponent ();
            BindingContext = new ViewModels.PlayerRegisterViewModel(Navigation);

        }
	}
}