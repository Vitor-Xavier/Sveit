using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Sveit.Helpers
{
    public class GenderTransalteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string gender)
            {
                switch (gender)
                {
                    case "Masculino": return AppResources.Male;
                    case "Feminino": return AppResources.Female;
                    case "Outros": return AppResources.Others;
                    default: return gender;
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
