using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Sveit.Models;
using Xamarin.Forms;

namespace Sveit.Helpers
{
    class SkillsCountToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ICollection<Skill> skills)
            {
                return string.Format(AppResources.SkillsRequired, skills.Count);
            }
            return string.Format(AppResources.SkillsRequired, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
