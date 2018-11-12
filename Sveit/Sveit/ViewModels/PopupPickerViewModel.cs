using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Sveit.Models;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    public class PopupPickerViewModel<T> : BaseViewModel
    {
        private readonly INavigation _navigation;

        public ObservableCollection<T> Items { get; set; }

        public event EventHandler<Xamarin.Forms.ItemTappedEventArgs> ItemSelected;

        private T item;

        public T Item
        {
            get { return item; }
            set { item = value; OnPropertyChanged(); ItemTapped(value); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }


        public ICommand CancelCommand => new Command(CancelCommandExecute);

        public PopupPickerViewModel(INavigation navigation, IList<T> list)
        {
            _navigation = navigation;
            Items = new ObservableCollection<T>();

            foreach (T it in list)
            {
                Items.Add(it);
            }
            //Items.Add(new Platform { Name = "XBOX One", Icon = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f9/Xbox_one_logo.svg/1024px-Xbox_one_logo.svg.png" });
            //Items.Add(new Platform { Name = "Playstation 4", Icon = "https://logodownload.org/wp-content/uploads/2017/05/playstation-4-logo-ps4-6.png" });
            //Items.Add(new Platform { Name = "PC", Icon = "https://pre00.deviantart.net/10db/th/pre/i/2017/235/5/4/pc_master_race_by_kingvego-d9r6gtn.png" });
        }

        private void ItemTapped(object obj)
        {
            if (obj != null)
                ItemSelected?.Invoke(this, new ItemTappedEventArgs(null, obj));
        }

        private void CancelCommandExecute()
        {
            _navigation.PopAllPopupAsync();
        }
    }
}
