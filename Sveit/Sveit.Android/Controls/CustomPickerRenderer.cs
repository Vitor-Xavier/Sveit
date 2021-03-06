﻿using Android.Content.Res;
using Sveit.Controls;
using Sveit.Droid.Controls;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace Sveit.Droid.Controls
{
    class CustomPickerRenderer : PickerRenderer
    {
        public CustomPickerRenderer(Android.Content.Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;
            var entry = (CustomPicker)e.NewElement;
            Control.BackgroundTintList = ColorStateList.ValueOf(entry.LineColor.ToAndroid());
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName != "LineColor") return;
            var entry = (CustomPicker)sender;
            Control.BackgroundTintList = ColorStateList.ValueOf(entry.LineColor.ToAndroid());
        }
    }
}