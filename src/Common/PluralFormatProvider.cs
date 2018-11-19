using System;
using Ardalis.GuardClauses;
using Common.Extensions;
using JetBrains.Annotations;

namespace Common
{
    public class PluralFormatProvider : IFormatProvider, ICustomFormatter
    {
        private const int SingularIndex = 0;
        private const int PluralIndex = 1;
        private const string Space = " ";

        public string Format(string format, [NotNull] object argument, IFormatProvider formatProvider) {
            Guard.Against.Null(() => argument);
            Guard.Against.Assert(() => argument.IsNumeric(), nameof(argument));

            if (format == null) format = string.Empty;

            var strings = format.Split(';');
            var hasStrings = strings.Length >= 2;
            var space = !string.IsNullOrEmpty(strings[0]) ? Space : string.Empty;

            var number = argument.ToInt32();
            var index = number.ToAbs() == 1 ? SingularIndex : PluralIndex;

            return hasStrings ? $"{number} {strings[index]}" : $"{number}{space}{strings[0]}";
        }

        public object GetFormat(Type formatType) => this;
    }
}