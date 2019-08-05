using System;
using System.IO;
using System.Text.RegularExpressions;
using Ardalis.GuardClauses;

// ReSharper disable MemberCanBePrivate.Global

namespace Common.Extensions
{
    public static class FileExtensions
    {
        private static readonly string InvalidChars = new string(Path.GetInvalidFileNameChars());

        public static string ToValidFileName(this Uri uri)
        {
            Guard.Against.Null(() => uri);

            return uri.OriginalString.ToValidFileName();
        }

        public static string ToValidFileName(this string str)
        {
            Guard.Against.NullOrWhiteSpace(() => str);

            var escapedInvalidChars = Regex.Escape(InvalidChars);
            var invalidCharsRegex = string.Format(@"([{0}]*\.+$)|([{0}]+)", escapedInvalidChars);

            return Regex.Replace(str, invalidCharsRegex, "_");
        }
    }
}