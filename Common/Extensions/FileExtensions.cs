using System;
using System.IO;
using System.Text.RegularExpressions;

// ReSharper disable MemberCanBePrivate.Global

namespace Common.Extensions
{
    public static class FileExtensions
    {
        public static string ToValidFileName(this Uri uri) => uri.OriginalString.ToValidFileName();

        public static string ToValidFileName(this string name) {
            var invalidChars = new string(Path.GetInvalidFileNameChars());
            var escapedInvalidChars = Regex.Escape(invalidChars);
            var invalidRegex = string.Format(@"([{0}]*\.+$)|([{0}]+)", escapedInvalidChars);

            return Regex.Replace(name, invalidRegex, "_");
        }
    }
}