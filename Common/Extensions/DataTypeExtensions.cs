using System;

namespace Common.Extensions
{
    public static class DataTypeExtensions
    {
        public static int? AsInt(this string value) => int.TryParse(value, out var result) ? (int?) result : null;

        public static DateTime? AsDateTime(this string value) =>
            DateTime.TryParse(value, out var result) ? (DateTime?) result : null;
    }
}