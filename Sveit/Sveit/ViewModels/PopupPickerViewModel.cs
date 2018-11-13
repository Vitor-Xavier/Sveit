using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Sveit.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using Sveit.Extensions;

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

        public IAsyncCommand CancelCommand => new AsyncCommand(CancelCommandExecute);

        public PopupPickerViewModel(INavigation navigation, IList<T> list)
        {
            _navigation = navigation;
            Items = new ObservableCollection<T>();

            foreach (T it in list)
                Items.Add(it);
        }

        private void ItemTapped(object obj)
        {
            if (obj != null)
                ItemSelected?.Invoke(this, new ItemTappedEventArgs(null, obj));
        }

        private async Task CancelCommandExecute()
        {
            await _navigation.PopAllPopupAsync();
        }
    }
}
