using System;
using System.Linq;
using System.Text;
using Ardalis.GuardClauses;
using JetBrains.Annotations;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveNumbers([NotNull] this string text) {
            Guard.Against.Null(() => text);

            return text.Where(c => !c.IsNumeric())
                .Aggregate(new StringBuilder(), (current, next) => current.Append(next), sb => sb.ToString());
        }

        public static bool NotNullNorEmpty([CanBeNull] this object value) => value is string str && str.NotNullNorEmpty();

        public static bool NotNullNorEmpty([CanBeNull] this string value) => !value.IsNullOrEmpty();

        public static bool IsNullOrEmpty([CanBeNull] this object value) => !(value is string str) || str.IsNullOrEmpty();

        public static bool IsNullOrEmpty([CanBeNull] this string value) => string.IsNullOrEmpty(value);

        public static bool NotNullNorWhiteSpace([CanBeNull] this object value) => value is string str && str.NotNullNorWhiteSpace();

        public static bool NotNullNorWhiteSpace([CanBeNull] this string value) => !value.IsNullOrWhiteSpace();

        public static bool IsNullOrWhiteSpace([CanBeNull] this object value) => !(value is string str) || str.IsNullOrWhiteSpace();

        public static bool IsNullOrWhiteSpace([CanBeNull] this string value) => string.IsNullOrWhiteSpace(value);

        public static string TruncateAtWord([CanBeNull] this string value, int length, [NotNull] string ellipsis = "…", [NotNull] string truncateAtChar = " ") {
            Guard.Against.OutOfRange(() => length, 1, int.MaxValue);
            Guard.Against.Null(() => ellipsis);
            Guard.Against.Null(() => truncateAtChar);

            if (value == null || value.Length <= length)
                return value;

            var nextSpaceIndex = value.LastIndexOf(truncateAtChar, length, StringComparison.Ordinal);
            var substring = $"{value.Substring(0, nextSpaceIndex > 0 ? nextSpaceIndex : length)}".Trim();
            substring = RemoveNonAlphaNumericTrailingCharacters(substring);

            return $"{substring}{ellipsis}";
        }

        private static string RemoveNonAlphaNumericTrailingCharacters(string text) {
            if (text.Length < 1 || text.LastOrDefault().ToString().IsPureAlphaNumeric()) return text;

            var substring = text.Substring(0, text.Length - 1);

            while (substring.Length > 0 && !substring.Last().ToString().IsPureAlphaNumeric()) substring = substring.Substring(0, substring.Length - 1);

            return substring;
        }
    }
}
