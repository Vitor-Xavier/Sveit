using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Sveit.Models;
using Xamarin.Forms;

namespace Sveit.Helpers
{
    class GenresToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ICollection<Genre> genres)
            {
                return string.Join(", ", genres);
            }
            return string.Empty;          
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
