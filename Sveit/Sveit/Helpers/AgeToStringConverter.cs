using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Sveit.Models;
using Xamarin.Forms;

namespace Sveit.Helpers
{
    class AgeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Vacancy vacancy)
            {
                if (vacancy.MinAge == 0 && vacancy.MaxAge == 0)
                    return AppResources.NoAgeRequirements;
                else if (vacancy.MinAge > 0 && vacancy.MaxAge == 0)
                    return string.Format(AppResources.AgeAbove, vacancy.MinAge);
                else if (vacancy.MaxAge > 0 && vacancy.MinAge == 0)
                    return string.Format(AppResources.AgeUp, vacancy.MaxAge);
                else
                    return string.Format(AppResources.AgeBetween, vacancy.MinAge, vacancy.MaxAge);
            }
            return AppResources.NoAgeRequirements;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
