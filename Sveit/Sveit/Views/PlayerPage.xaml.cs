using Sveit.Services.Requests;
using Sveit.Utils;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerPage : ContentPage
    {
        public PlayerPage()
        {
            InitializeComponent();
            //BindingContext = new ViewModels.PlayerViewModel(requestService, playerId);
            //(BindingContext as ViewModels.PlayerViewModel).ToolbarIsVisibleChanged += this.ToolbarIsVisibleChanged;
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

        //public PlayerPage(IRequestService requestService) : this(requestService, null) { }

        public void ToolbarIsVisibleChanged(object sender, BoolChangedEventArgs e)
        {
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
    }
}