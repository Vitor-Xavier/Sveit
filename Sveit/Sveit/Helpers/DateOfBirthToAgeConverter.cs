using System;
using System.Globalization;
using Xamarin.Forms;

namespace Sveit.Helpers
{
    public class DateOfBirthToAgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateOfBirth)
            {
                TimeSpan ts = DateTime.Now.Subtract(dateOfBirth);
                DateTime age = new DateTime(ts.Ticks);
                return age.Year - 1;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
