using Sveit.Services.Requests;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerPage : ContentPage
    {
        public PlayerPage(IRequestService requestService, int? playerId)
        {
            InitializeComponent();
            BindingContext = new ViewModels.PlayerViewModel(Navigation, requestService, playerId);

            List<ToolbarItem> items = new List<ToolbarItem>();
            foreach (Sveit.Controls.HideableToolbarItem toolbarItem in ToolbarItems)
            {
                if (toolbarItem.IsVisible == false)
                {
                    items.Add(toolbarItem);
                }
            }
            foreach (Sveit.Controls.HideableToolbarItem toolbarItem in items)
            {
                ToolbarItems.Remove(toolbarItem);
            }
        }

        public PlayerPage(IRequestService requestService) : this(requestService, null) { }

    }
}