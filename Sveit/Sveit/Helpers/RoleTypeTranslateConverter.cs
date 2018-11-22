using System;
using System.Globalization;
using Xamarin.Forms;

namespace Sveit.Helpers
{
    public class RoleTypeTranslateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text)
            {
                switch (text)
                {
                    case "Primary Role": return AppResources.PrimaryRole;
                    case "Secondary Role": return AppResources.SecondaryRole;
                    default: return text;
                }
            };
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
