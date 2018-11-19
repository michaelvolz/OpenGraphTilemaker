using System;
using System.Linq;
using Ardalis.GuardClauses;
using JetBrains.Annotations;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static bool NotNullNorEmpty([CanBeNull] this object value) {
            return value is string str && str.NotNullNorEmpty();
        }

        public static bool NotNullNorEmpty([CanBeNull] this string value) {
            return !value.IsNullOrEmpty();
        }

        public static bool IsNullOrEmpty([CanBeNull] this object value) {
            return !(value is string str) || str.IsNullOrEmpty();
        }

        public static bool IsNullOrEmpty([CanBeNull] this string value) {
            return string.IsNullOrEmpty(value);
        }

        public static bool NotNullNorWhiteSpace([CanBeNull] this object value) {
            return value is string str && str.NotNullNorWhiteSpace();
        }

        public static bool NotNullNorWhiteSpace([CanBeNull] this string value) {
            return !value.IsNullOrWhiteSpace();
        }

        public static bool IsNullOrWhiteSpace([CanBeNull] this object value) {
            return !(value is string str) || str.IsNullOrWhiteSpace();
        }

        public static bool IsNullOrWhiteSpace([CanBeNull] this string value) {
            return string.IsNullOrWhiteSpace(value);
        }

        public static string TruncateAtWord([CanBeNull] this string value, int length, [NotNull] string ellipsis = "…", [NotNull] string truncateAtChar = " ") {
            Guard.Against.OutOfRange(() => length, 1, int.MaxValue);
            Guard.Against.Null(() => ellipsis);
            Guard.Against.Null(() => truncateAtChar);

            if (value == null || value.Length <= length)
                return value;

            var nextSpaceIndex = value.LastIndexOf(truncateAtChar, length, StringComparison.Ordinal);
            var truncatedString = $"{value.Substring(0, nextSpaceIndex > 0 ? nextSpaceIndex : length)}".Trim();
            var lastCharacter = truncatedString.Last().ToString();

            return lastCharacter.IsPureAlphaNumeric() ? $"{truncatedString}{ellipsis}" : $"{truncatedString} {ellipsis}";
        }
    }
}
