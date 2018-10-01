using Sveit.Controls;
using Xamarin.Forms;

namespace Sveit.Behaviors
{
    public class TextLenghtValidationBehavior : Behavior<CustomEntry>
    {
        public BindableProperty MinLengthProperty = BindableProperty.Create(nameof(MinLength), typeof(int), typeof(TextLenghtValidationBehavior), 0, BindingMode.TwoWay);

        public int MinLength
        {
            get => (int)GetValue(MinLengthProperty);
            set => SetValue(MinLengthProperty, value);
        }

        protected override void OnAttachedTo(CustomEntry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsValid = false;
            IsValid = e.NewTextValue.Length >= MinLength;

            ((CustomEntry)sender).LineColor = IsValid ? Color.Default : Color.Red;
        }

        protected override void OnDetachingFrom(CustomEntry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }

    }
}
