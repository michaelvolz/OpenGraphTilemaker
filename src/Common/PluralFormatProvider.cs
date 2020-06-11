using System;
using Ardalis.GuardClauses;
using Common.Extensions;

namespace Common
{
    public class PluralFormatProvider : IFormatProvider, ICustomFormatter
    {
        private const int SingularIndex = 0;
        private const int PluralIndex = 1;
        private const string Space = " ";

        public string Format(string? format, object? arg, IFormatProvider? formatProvider)
        {
            Guard.Against.Null(arg, nameof(arg));
            Guard.Against.Assert(() => arg!.IsNumeric(), nameof(arg));

            format ??= string.Empty;

            var strings = format.Split(';');
            var hasStrings = strings.Length >= 2;
            var space = !string.IsNullOrEmpty(strings[0]) ? Space : string.Empty;

            var number = arg!.ToInt32();
            var index = number.ToAbs() == 1 ? SingularIndex : PluralIndex;

            return hasStrings ? $"{number} {strings[index]}" : $"{number}{space}{strings[0]}";
        }

        public object? GetFormat(Type? formatType) => this;
    }
}