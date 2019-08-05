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

        public static string ToFriendlyDate(this DateTime? date) => date.HasValue ? date.Value.ToFriendlyDate() : string.Empty;

        public static string ToFriendlyDate(this DateTime date)
        {
            var elapsedTime = DateTime.UtcNow.Subtract(date);
            var totalDaysElapsed = (int)elapsedTime.TotalDays;
            var totalSecondsElapsed = (int)elapsedTime.TotalSeconds;

            return totalDaysElapsed switch
            {
                int _ when totalSecondsElapsed < 0 => "n/a",

                Today when totalSecondsElapsed < MinuteInSeconds => "just now",
                Today when totalSecondsElapsed < HourInSeconds => string.Format(new PluralFormatProvider(), "{0:minute;minutes} ago",
                    totalSecondsElapsed.FloorBy(MinuteInSeconds)),
                Today when totalSecondsElapsed < DayInSeconds => string.Format(new PluralFormatProvider(), "{0:hour;hours} ago",
                    totalSecondsElapsed.FloorBy(HourInSeconds)),

                Yesterday => "yesterday",

                int days when days < WeekInDays => $"{totalDaysElapsed} days ago",
                int days when days < MonthInDays => string.Format(new PluralFormatProvider(), "{0:week;weeks} ago",
                    totalDaysElapsed.CeilingBy(WeekInDays)),

                _ => date.ToLongDateString()
            };
        }
    }
}