using System;
using System.Globalization;

namespace OpenGraphTilemaker
{
    public static class DateTimeExtensions
    {
        private const int Today = 0;
        private const int Yesterday = 1;
        private const int Week = 7;
        private const int Month = 31;

        private const int OneMinute = 60;
        private const int OneHour = 3600;
        private const int OneDay = 86400;

        public static string ToFriendlyDate(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToFriendlyDate() : string.Empty;
        }

        public static string ToFriendlyDate(this DateTime date)
        {
            var elapsedTime = DateTime.UtcNow.Subtract(date);
            var totalDaysElapsed = (int) elapsedTime.TotalDays;
            var totalSecondsElapsed = (int) elapsedTime.TotalSeconds;

            switch (totalDaysElapsed)
            {
                case int _ when totalSecondsElapsed < 0 : return "n/a";
                
                case Today when totalSecondsElapsed < OneMinute: return "just now";

                case Today when totalSecondsElapsed < OneHour:
                    return string.Format(new PluralFormatProvider(), "{0:minute;minutes} ago",
                        totalSecondsElapsed.FloorBy(OneMinute));

                case Today when totalSecondsElapsed < OneDay:
                    return string.Format(new PluralFormatProvider(), "{0:hour;hours} ago",
                        totalSecondsElapsed.FloorBy(OneHour));

                case Yesterday: return "yesterday";

                case int days when days < Week: return $"{totalDaysElapsed} days ago";

                case int days when days < Month:
                    return string.Format(new PluralFormatProvider(), "{0:week;weeks} ago",
                        totalDaysElapsed.CeilingBy(Week));

                default:
                    return date.ToString(CultureInfo.InvariantCulture);
            }
        }

        private static double CeilingBy(this int dividend, int divisor) => Math.Ceiling((double) dividend / divisor);

        private static double FloorBy(this int dividend, int divisor) => Math.Floor((double) dividend / divisor);

        private class PluralFormatProvider : IFormatProvider, ICustomFormatter
        {
            public string Format(string format, object arg, IFormatProvider formatProvider) =>
                $"{Convert.ToInt32(arg)} {format.Split(';')[Convert.ToInt32(arg) == 1 ? 0 : 1]}";

            public object GetFormat(Type formatType) => this;
        }
    }
}