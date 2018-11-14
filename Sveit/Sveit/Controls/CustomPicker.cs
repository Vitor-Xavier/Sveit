using Xamarin.Forms;

namespace Sveit.Controls
{
    public class CustomPicker : Picker
    {
        public BindableProperty LineColorProperty =
            BindableProperty.Create(nameof(LineColor), typeof(Color), typeof(CustomPicker), Color.White);

        public Color LineColor
        {
            get => (Color)GetValue(LineColorProperty);
            set => SetValue(LineColorProperty, value);
        }
    }
}
