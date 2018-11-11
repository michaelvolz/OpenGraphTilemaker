using System;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static string TruncateAtWord(this string value, int length, string truncateAtChar = " ") {
            if (value == null || value.Length <= length)
                return value;

            var nextSpaceIndex = value.LastIndexOf(truncateAtChar, length, StringComparison.Ordinal);
            return $"{value.Substring(0, nextSpaceIndex > 0 ? nextSpaceIndex : length).Trim()} ...";
        }
    }
}