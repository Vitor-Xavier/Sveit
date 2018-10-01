using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Sveit.Models;
using Xamarin.Forms;

namespace Sveit.Helpers
{
    class ContactsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ICollection<Contact> contacts)
            {
                return contacts.Count != 0 ? string.Join(", ", contacts) : AppResources.NoContacts;
            }
            return AppResources.NoContacts;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
