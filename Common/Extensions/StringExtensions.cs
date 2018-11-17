using System;
using JetBrains.Annotations;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static bool NotNullOrWhiteSpace([CanBeNull] this string value) {
            return !value.IsNullOrWhiteSpace();
        }

        public static bool IsNullOrWhiteSpace([CanBeNull] this string value) {
            return string.IsNullOrWhiteSpace(value);
        }

        public static string TruncateAtWord(this string value, int length, string truncateAtChar = " ") {
            if (value == null || value.Length <= length)
                return value;

            var nextSpaceIndex = value.LastIndexOf(truncateAtChar, length, StringComparison.Ordinal);
            return $"{value.Substring(0, nextSpaceIndex > 0 ? nextSpaceIndex : length).Trim()} ...";
        }
    }
}