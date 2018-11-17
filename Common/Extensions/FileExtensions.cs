using System;
using System.IO;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

// ReSharper disable MemberCanBePrivate.Global

namespace Common.Extensions
{
    public static class FileExtensions
    {
        public static string ToValidFileName([NotNull] this Uri uri) {
            if (uri == null) throw new ArgumentNullException(nameof(uri));
            
            return uri.OriginalString.ToValidFileName();
        }

        public static string ToValidFileName([NotNull] this string name) {
            if (name == null) throw new ArgumentNullException(nameof(name));
            
            var invalidChars = new string(Path.GetInvalidFileNameChars());
            var escapedInvalidChars = Regex.Escape(invalidChars);
            var invalidRegex = string.Format(@"([{0}]*\.+$)|([{0}]+)", escapedInvalidChars);

            return Regex.Replace(name, invalidRegex, "_");
        }
    }
}