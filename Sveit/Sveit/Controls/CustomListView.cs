using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Sveit.Controls
{
    public class CustomListView : ListView
    {
        public static readonly BindableProperty ItemTappedCommandProperty =
            BindableProperty.Create("ItemTappedCommand",
                typeof(ICommand),
                typeof(CustomListView),
                null);

        public ICommand ItemTappedCommand
        {
            get { return (ICommand)GetValue(ItemTappedCommandProperty); }
            set
            {
                SetValue(ItemTappedCommandProperty, value);
            }
        }

        public CustomListView() : this(ListViewCachingStrategy.RecycleElement) { }

        public CustomListView(ListViewCachingStrategy strategy) : base(strategy)
        {
            Initialize();
        }

        private void Initialize()
        {
            this.ItemSelected += (sender, e) =>
            {
                if (ItemTappedCommand == null) return;

                if (ItemTappedCommand.CanExecute(e.SelectedItem))
                    ItemTappedCommand.Execute(e.SelectedItem);
                SelectedItem = null;
            };
        }
    }
}
