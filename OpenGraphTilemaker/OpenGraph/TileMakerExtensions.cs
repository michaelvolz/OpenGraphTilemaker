using System;
using System.IO;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemaker.OpenGraph
{
    public static class TileMakerExtensions
    {
        public static string TruncateAtWord(this string value, int length, string truncateAtChar = " ") {
            if (value == null || value.Length <= length)
                return value;

            var nextSpaceIndex = value.LastIndexOf(truncateAtChar, length, StringComparison.Ordinal);
            return $"{value.Substring(0, nextSpaceIndex > 0 ? nextSpaceIndex : length).Trim()} ...";
        }

        public static string ToValidFileName(this Uri uri) => uri.OriginalString.ToValidFileName();

        public static string ToValidFileName(this string name) {
            var invalidChars = new string(Path.GetInvalidFileNameChars());
            var escapedInvalidChars = Regex.Escape(invalidChars);
            var invalidRegex = string.Format(@"([{0}]*\.+$)|([{0}]+)", escapedInvalidChars);

            return Regex.Replace(name, invalidRegex, "_");
        }

        public static int? AsInt(this string value) => int.TryParse(value, out var result) ? (int?) result : null;

        public static DateTime? AsDateTime(this string value) =>
            DateTime.TryParse(value, out var result) ? (DateTime?) result : null;

        public static string DeEntitize(this string value) => HtmlEntity.DeEntitize(value);
    }
}