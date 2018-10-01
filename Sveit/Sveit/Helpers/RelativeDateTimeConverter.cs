using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Sveit.Helpers
{
    public class RelativeDateTimeConverter : IValueConverter
    {
        private const int SECOND = 1;
        private const int MINUTE = 60 * SECOND;
        private const int HOUR = 60 * MINUTE;
        private const int DAY = 24 * HOUR;
        private const int MONTH = 30 * DAY;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            var postedData = (DateTime)value;

            var ts = new TimeSpan(DateTime.Now.Ticks - postedData.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
            {
                if (ts.Seconds <= 0)
                {
                    return AppResources.JustNow;
                }
                return ts.Seconds == 1 ? AppResources.OneSecAgo : string.Format(AppResources.SecsAgo, ts.Seconds);
            }

            if (delta < 2 * MINUTE)
                return AppResources.AMinAgo;

            if (delta < 45 * MINUTE)
            {
                if (ts.Seconds < 0)
                {
                    return AppResources.SometimeAgo;
                }
                return string.Format(AppResources.MinsAgo, ts.Minutes);
            }

            if (delta <= 90 * MINUTE)
                return AppResources.AnHourAgo;

            if (delta < 24 * HOUR)
            {
                if (ts.Hours < 0)
                {
                    return AppResources.SometimeAgo;
                }

                if (ts.Hours == 1)
                    return AppResources.AnHourAgo;
                return string.Format(AppResources.HoursAgo, ts.Hours);
            }

            if (delta < 48 * HOUR)
                return string.Format(AppResources.YesterdayAt, postedData.ToString("t"));

            if (delta < 30 * DAY)
            {
                if (ts.Days == 1)
                    return AppResources.OneDayAgo;
                return string.Format(AppResources.DaysAgo, ts.Days);
            }


            if (delta < 12 * MONTH)
            {
                int months = (int)(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? AppResources.OneMonthAgo : string.Format(AppResources.MonthsAgo, months);
            }
            else
            {
                int years = (int)(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? AppResources.OneYearAgo : string.Format(AppResources.YearsAgo, years);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
