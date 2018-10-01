using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Sveit.Controls
{
    public class CustomEntry : Entry
    {
        public BindableProperty LineColorProperty = 
            BindableProperty.Create(nameof(LineColor), typeof(Color), typeof(CustomEntry), Color.White);

        public Color LineColor
        {
            get => (Color) GetValue(LineColorProperty); 
            set => SetValue(LineColorProperty, value); 
        }
    }
}
