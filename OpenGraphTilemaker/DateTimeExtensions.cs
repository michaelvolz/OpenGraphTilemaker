using System;
using System.Globalization;
using System.Linq;

namespace OpenGraphTilemaker
{
    public static class DateTimeExtensions
    {
        private const int Today = 0;
        private const int Yesterday = 1;
        private const int WeekInDays = 7;
        private const int MonthInDays = 31;

        private const int MinuteInSeconds = 60;
        private const int HourInSeconds = 3600;
        private const int DayInSeconds = 86400;

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
                case int _ when totalSecondsElapsed < 0: return "n/a";

                case Today when totalSecondsElapsed < MinuteInSeconds: return "just now";

                case Today when totalSecondsElapsed < HourInSeconds:
                    return string.Format(new PluralFormatProvider(), "{0:minute;minutes} ago",
                        totalSecondsElapsed.FloorBy(MinuteInSeconds));

                case Today when totalSecondsElapsed < DayInSeconds:
                    return string.Format(new PluralFormatProvider(), "{0:hour;hours} ago",
                        totalSecondsElapsed.FloorBy(HourInSeconds));

                case Yesterday: return "yesterday";

                case int days when days < WeekInDays: return $"{totalDaysElapsed} days ago";

                case int days when days < MonthInDays:
                    return string.Format(new PluralFormatProvider(), "{0:week;weeks} ago",
                        totalDaysElapsed.CeilingBy(WeekInDays));

                default:
                    return date.ToLongDateString();
            }
        }

        private static double CeilingBy(this int dividend, int divisor) => Math.Ceiling((double) dividend / divisor);

        private static double FloorBy(this int dividend, int divisor) => Math.Floor((double) dividend / divisor);

        private static int ToInt32(this object arg) => Convert.ToInt32(arg);

        private static int ToAbs(this int arg) => Math.Abs(arg);

        // ReSharper disable once MemberCanBePrivate.Global
        public class PluralFormatProvider : IFormatProvider, ICustomFormatter
        {
            private const int Singular = 0;
            private const int Plural = 1;
            protected internal const string Space = " ";

            public string Format(string format, object arg, IFormatProvider formatProvider)
            {
                if (format == null) format = string.Empty;
                if (arg == null) throw new ArgumentNullException(nameof(arg));

                var strings = format.Split(';');
                var hasStrings = strings.Length >= 2;
                var space = strings[0] != string.Empty ? Space : string.Empty;

                var number = arg.ToInt32();
                var index = number.ToAbs() == 1 ? Singular : Plural;
                
                return hasStrings ? $"{number} {strings[index]}" : $"{number}{space}{strings[0]}";
            }

            public object GetFormat(Type formatType) => this;
        }
    }
}