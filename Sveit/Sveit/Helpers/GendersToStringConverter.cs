using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Sveit.Models;
using Xamarin.Forms;

namespace Sveit.Helpers
{
    class GendersToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ICollection<Gender> genders)
            {
                var translatedGenders = new List<Gender>();
                foreach (Gender g in genders)
                    translatedGenders.Add(new Gender { Name = GetGenderTranslated(g.Name) });
                return translatedGenders.Count == 0 ? AppResources.NoRequirements : string.Join(", ", translatedGenders);
            }
            return AppResources.NoRequirements;
        }

        private string GetGenderTranslated(string gender)
        {
            switch (gender)
            {
                case "Masculino": return AppResources.Male;
                case "Feminino": return AppResources.Female;
                case "Outros": return AppResources.Others;
                default: return gender;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
