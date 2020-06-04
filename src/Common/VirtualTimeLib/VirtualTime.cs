// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace VirtualTimeLib
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
    internal class VirtualTime : ITime
    {
        public VirtualTime(DateTime whenTimeStarts, double speedOfTimePerMs = 1, int marginOfErrorMs = 10)
        {
            WhenTimeStarts = whenTimeStarts;
            SpeedOfTimePerMs = speedOfTimePerMs;
            MarginOfErrorMs = marginOfErrorMs;
            InitialTimeUtc = DateTime.UtcNow;
            InitialTime = DateTime.Now;
        }

        public VirtualTime() => UseRealTime = true;

        public bool UseRealTime { get; set; }

        public DateTime Now => GetVirtualTime(DateTime.Now);

        public DateTime UtcNow => GetVirtualTime(DateTime.UtcNow, false, true);

        public DateTime Today => GetVirtualTime(DateTime.Today);

        public DateTime FromFileTime(long value) => DateTime.FromFileTime(value);

        public DateTime FromBinary(long dateData) => DateTime.FromBinary(dateData);

        public DateTime FromFileTimeUtc(long fileTimeUtc) => DateTime.FromFileTimeUtc(fileTimeUtc);

        public DateTime Parse(string s) => DateTime.Parse(s, CultureInfo.InvariantCulture);

        public DateTime FromOADate(double s) => DateTime.FromOADate(s);

        public DateTime ParseExact(string s, string format, IFormatProvider formatProvider) => DateTime.ParseExact(s, format, formatProvider);

        public DateTime SpecifyKind(DateTime value, DateTimeKind kind) => DateTime.SpecifyKind(value, kind);

        public int Compare(DateTime t1, DateTime t2) => DateTime.Compare(t1, t2);

        public int DaysInMonth(int year, int month) => DateTime.DaysInMonth(year, month);

        public bool Equals(DateTime t1, DateTime t2) => DateTime.Equals(t1, t2);

        public bool TryParse(string s, out DateTime date) => DateTime.TryParse(s, out date);

        public bool IsLeapYear(int year) => DateTime.IsLeapYear(year);

        public DateTime MinValue => DateTime.MinValue;

        public DateTime MaxValue => DateTime.MaxValue;

        // public DateTime GetVirtualTimeEquivalent(DateTime dateTime) => GetVirtualTime(dateTime,true);

        private DateTime GetVirtualTime(DateTime time, bool force = false, bool isUtc = false)
        {
            if (UseRealTime && !force)
                return time;

            var elapsed = (time - (isUtc ? InitialTimeUtc : InitialTime)).TotalMilliseconds;
            var elapsedMilliseconds = MarginOfErrorMs == 0 ? Math.Floor(elapsed) : Math.Floor(elapsed / MarginOfErrorMs) * MarginOfErrorMs;
            var virtualElapseTime = elapsedMilliseconds * SpeedOfTimePerMs;

            return WhenTimeStarts.AddMilliseconds(virtualElapseTime);
        }

        private DateTime WhenTimeStarts { get; }
    
        private double SpeedOfTimePerMs { get; }

        private DateTime InitialTimeUtc { get; }

        private DateTime InitialTime { get; }

        private int MarginOfErrorMs { get; }
    }
}