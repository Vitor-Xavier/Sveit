﻿using System;
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
                if (skills.Count != 0)
                    return string.Format(AppResources.SkillsRequired, skills.Count);
            }
            return AppResources.NoSkillsRequired;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
