using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Sveit.Helpers
{
    public class RoleTranslateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text)
            {
                switch (text)
                {
                    case "DPS": return AppResources.DPS;
                    case "Tanque": return AppResources.Tank;
                    case "Suporte": return AppResources.Support;
                    case "Agressivo": return AppResources.Aggressive;
                    case "Objetivo": return AppResources.Objective;
                    default: return text;
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
