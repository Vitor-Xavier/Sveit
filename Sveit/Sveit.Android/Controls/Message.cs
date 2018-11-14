
using Android.App;
using Android.Widget;
using Sveit.Controls;

[assembly: Xamarin.Forms.Dependency(typeof(Sveit.Droid.Controls.Message))]
namespace Sveit.Droid.Controls
{
    public class Message : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}