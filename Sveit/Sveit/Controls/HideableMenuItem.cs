using Xamarin.Forms;

namespace Sveit.Controls
{
    public class HideableMenuItem : MenuItem
    {
        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        public static BindableProperty IsVisibleProperty =
            BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(HideableMenuItem), true);
    }
}
