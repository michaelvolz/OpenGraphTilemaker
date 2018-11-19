using System;

// ReSharper disable UnusedMember.Global

namespace Common.Extensions
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

        public static string ToFriendlyDate(this DateTime? date) {
            return date.HasValue ? date.Value.ToFriendlyDate() : string.Empty;
        }

        public static string ToFriendlyDate(this DateTime date) {
            var elapsedTime = DateTime.UtcNow.Subtract(date);
            var totalDaysElapsed = (int)elapsedTime.TotalDays;
            var totalSecondsElapsed = (int)elapsedTime.TotalSeconds;

            switch (totalDaysElapsed) {
                case int _ when totalSecondsElapsed < 0: return "n/a";

                case Today when totalSecondsElapsed < MinuteInSeconds: return "just now";

                case Today when totalSecondsElapsed < HourInSeconds:
                    return string.Format(new PluralFormatProvider(), "{0:minute;minutes} ago", totalSecondsElapsed.FloorBy(MinuteInSeconds));

                case Today when totalSecondsElapsed < DayInSeconds:
                    return string.Format(new PluralFormatProvider(), "{0:hour;hours} ago", totalSecondsElapsed.FloorBy(HourInSeconds));

                case Yesterday: return "yesterday";

                case int days when days < WeekInDays: return $"{totalDaysElapsed} days ago";

                case int days when days < MonthInDays:
                    return string.Format(new PluralFormatProvider(), "{0:week;weeks} ago", totalDaysElapsed.CeilingBy(WeekInDays));

                default:
                    return date.ToLongDateString();
            }
        }
    }
}