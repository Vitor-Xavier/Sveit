using Android.Content;
using Sveit.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ListView), typeof(ListViewNestedScroll))]
namespace Sveit.Droid.Controls
{
    public class ListViewNestedScroll : ListViewRenderer
    {
        public ListViewNestedScroll(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var listView = this.Control as Android.Widget.ListView;
                listView.NestedScrollingEnabled = true;
            }
        }
    }
}