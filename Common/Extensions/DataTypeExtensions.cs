using System;
using System.Globalization;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

// ReSharper disable UnusedMember.Global

namespace Common.Extensions
{
    public static class DataTypeExtensions
    {
        public static int ToInt32(this object arg) => Convert.ToInt32(arg);

        [CanBeNull]
        public static int? AsIntOrNull(this string value) => int.TryParse(value, out var result) ? (int?) result : null;

        [CanBeNull]
        public static DateTime? AsDateTimeOrNull(this string value) => DateTime.TryParse(value, out var result) ? (DateTime?) result : null;

        /// <summary>
        ///     Checks for a-z A-Z 0-9 only
        /// </summary>
        public static bool IsPureAlphaNumeric(this string value) {
            var rg = new Regex(@"^[a-zA-Z0-9]*$");
            return rg.IsMatch(value);
        }

        /// <summary>
        ///     Checks for a-z A-Z 0-9 spaces and commas
        /// </summary>
        public static bool IsAlphaNumeric(this string value) {
            var rg = new Regex(@"^[a-zA-Z0-9\s,]*$");
            return rg.IsMatch(value);
        }

        public static bool IsNumeric(this object expression) {
            if (expression == null) return false;

            return double.TryParse(
                Convert.ToString(expression, CultureInfo.InvariantCulture),
                NumberStyles.Any,
                NumberFormatInfo.InvariantInfo,
                out _);
        }
    }
}