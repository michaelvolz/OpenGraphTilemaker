using System;

namespace Common
{
    public class PluralFormatProvider : IFormatProvider, ICustomFormatter
    {
        private const int Singular = 0;
        private const int Plural = 1;
        private const string Space = " ";

        public string Format(string format, object arg, IFormatProvider formatProvider) {
            if (format == null) format = string.Empty;
            if (arg == null) throw new ArgumentNullException(nameof(arg));

            var strings = format.Split(';');
            var hasStrings = strings.Length >= 2;
            var space = strings[0] != string.Empty ? Space : string.Empty;

            var number = arg.ToInt32();
            var index = number.ToAbs() == 1 ? Singular : Plural;

            return hasStrings ? $"{number} {strings[index]}" : $"{number}{space}{strings[0]}";
        }

        public object GetFormat(Type formatType) => this;
    }
}