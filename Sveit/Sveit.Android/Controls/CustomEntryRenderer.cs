using Android.Content.Res;
using System.ComponentModel;
using Sveit.Controls;
using Sveit.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace Sveit.Droid.Controls
{
    class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Android.Content.Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;
            var entry = (CustomEntry)e.NewElement;
            Control.BackgroundTintList = ColorStateList.ValueOf(entry.LineColor.ToAndroid());
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName != "LineColor") return;
            var entry = (CustomEntry)sender;
            Control.BackgroundTintList = ColorStateList.ValueOf(entry.LineColor.ToAndroid());
        }
    }
}