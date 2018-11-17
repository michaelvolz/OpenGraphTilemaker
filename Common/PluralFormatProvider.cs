using System;
using Common.Extensions;

namespace Common
{
    public class PluralFormatProvider : IFormatProvider, ICustomFormatter
    {
        private const int SingularIndex = 0;
        private const int PluralIndex = 1;
        private const string Space = " ";

        public string Format(string format, object argument, IFormatProvider formatProvider) {
            if (format == null) format = string.Empty;
            if (argument == null) throw new ArgumentNullException(nameof(argument));
            if (!argument.IsNumeric()) throw new ArgumentOutOfRangeException(nameof(argument), "'argument' needs to be numeric!");

            var strings = format.Split(';');
            var hasStrings = strings.Length >= 2;
            var space = strings[0] != string.Empty ? Space : string.Empty;

            var number = argument.ToInt32();
            var index = number.ToAbs() == 1 ? SingularIndex : PluralIndex;

            return hasStrings ? $"{number} {strings[index]}" : $"{number}{space}{strings[0]}";
        }

        public object GetFormat(Type formatType) => this;
    }
}