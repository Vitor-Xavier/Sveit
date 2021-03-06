﻿using Sveit.Services.Requests;
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
	public partial class GamePage : ContentPage
	{
        public GamePage(IRequestService requestService, Models.Game game)
		{
			InitializeComponent();
            BindingContext = new Sveit.ViewModels.GameViewModel(Navigation, requestService, game);
		}
    }
}