using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Ardalis.GuardClauses;

namespace Common.Extensions
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static class FileExtensions
    {
        private static readonly string InvalidChars = new string(Path.GetInvalidFileNameChars());

        public static string ToValidFileName(this Uri uri)
        {
            Guard.Against.Null(uri, nameof(uri));

            return uri.OriginalString.ToValidFileName();
        }

        public static string ToValidFileName(this string str)
        {
            Guard.Against.NullOrWhiteSpace(() => str);

            var escapedInvalidChars = Regex.Escape(InvalidChars);
            var invalidCharsRegex = string.Format(CultureInfo.InvariantCulture, @"([{0}]*\.+$)|([{0}]+)", escapedInvalidChars);

            return Regex.Replace(str, invalidCharsRegex, "_");
        }
    }
}