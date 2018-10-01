using System;
using System.Collections.Generic;
using System.Text;
using Sveit.Controls;
using Xamarin.Forms;

namespace Sveit.Behaviors
{
    class PickerSelectedBehavior : Behavior<CustomPicker>
    {
        protected override void OnAttachedTo(CustomPicker bindable)
        {
            bindable.SelectedIndexChanged += HandleIndexChanged;
            base.OnAttachedTo(bindable);
        }

        void HandleIndexChanged(object sender, EventArgs e)
        {
            if (sender is CustomPicker picker)
            {
                bool IsValid = picker.SelectedItem != null;

                ((CustomPicker)sender).LineColor = IsValid ? Color.Default : Color.Red;
            }
        }

        protected override void OnDetachingFrom(CustomPicker bindable)
        {
            bindable.SelectedIndexChanged -= HandleIndexChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
