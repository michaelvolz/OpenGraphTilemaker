﻿using System;
using System.Linq;
using System.Text;
using Ardalis.GuardClauses;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        private const string Space = " ";

        public static string CombineAll(this string[] texts, string combineWith = Space) =>
            texts.Aggregate(string.Empty, (current, next) => $"{current}{combineWith}{next}");

        public static string RemoveHashFromHashTag(this string? word) =>
            string.IsNullOrEmpty(word) ? string.Empty : word.TrimStart('#');

        public static string RemoveAlternateQuotationMarks(this string? word) =>
            string.IsNullOrEmpty(word) ? string.Empty : word.TrimStart('“').TrimEnd('”');

        public static string RemoveTrailingPunctuation(this string? word)
        {
            if (string.IsNullOrEmpty(word)) return string.Empty;

            var result = new StringBuilder(word);

            for (var index = word.Length - 1; index >= 0; index--)
            {
                if (!char.IsPunctuation(word[index])) break;

                result.Remove(index, 1);
            }

            return result.ToString();
        }

        public static string RemoveNumbers(this string text)
        {
            Guard.Against.Null(text, nameof(text));

            return text.Where(c => !c.IsNumeric()).Aggregate(new StringBuilder(), (current, next) => current.Append(next), sb => sb.ToString());
        }

        public static bool NotNullNorEmpty(this object? value) => value is string str && str.NotNullNorEmpty();

        public static bool NotNullNorEmpty(this string? value) => !value.IsNullOrEmpty();

        public static bool IsNullOrEmpty(this object? value) => string.IsNullOrEmpty(value as string);

        public static bool IsNullOrEmpty(this string? value) => string.IsNullOrEmpty(value);

        public static bool NotNullNorWhiteSpace(this object? value) => value is string str && str.NotNullNorWhiteSpace();

        public static bool NotNullNorWhiteSpace(this string? value) => !value.IsNullOrWhiteSpace();

        public static bool IsNullOrWhiteSpace(this object? value) => string.IsNullOrWhiteSpace(value as string);

        public static bool IsNullOrWhiteSpace(this string? value) => string.IsNullOrWhiteSpace(value);

        public static string? TruncateAtWord(this string? value, int length, string ellipsis = "…", string truncateAtChar = " ")
        {
            Guard.Against.OutOfRange(() => length, 1, int.MaxValue);
            Guard.Against.Null(truncateAtChar, nameof(truncateAtChar));
            Guard.Against.Null(ellipsis, nameof(ellipsis));

            if (value == null || value.Length <= length)
                return value;

            var nextSpaceIndex = value.LastIndexOf(truncateAtChar, length, StringComparison.Ordinal);
            var substring = value.Substring(0, nextSpaceIndex > 0 ? nextSpaceIndex : length).Trim();
            substring = RemoveNonAlphaNumericTrailingCharacters(substring);

            return $"{substring}{ellipsis}";
        }

        private static string RemoveNonAlphaNumericTrailingCharacters(string text)
        {
            if (text.Length < 1 || text.LastOrDefault().ToString().IsPureAlphaNumeric()) return text;

            var substring = text[..^1];

            while (substring.Length > 0 && !substring.Last().ToString().IsPureAlphaNumeric())
                substring = substring[..^1];

            return substring;
        }
    }
}
