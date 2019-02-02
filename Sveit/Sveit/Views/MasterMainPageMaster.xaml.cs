﻿
using Sveit.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sveit.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterMainPageMaster : ContentPage
    {
        public ListView ListView;

        public MasterMainPageMaster()
        {
            InitializeComponent();

            //BindingContext = new ViewModels.MasterMainViewModel(requestService);
            ListView = MenuItemsListView;
            ListView.ItemSelected += ListView_ItemSelected;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(e.SelectedItem is MasterMenuItem item))
                return;
            await (BindingContext as MasterMainViewModel).ItemSelected(item);

            ListView.SelectedItem = null;
        }
    }
}