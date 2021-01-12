﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.Extensions;
using static Common.Extensions.AssemblyExtensions;

namespace Common.TagCloud
{
    public class Tags
    {
        private const string MySQLMyISAMText = @"TagCloud\mysql_myisam.txt";

        private static string[]? _stopWords;

        public Dictionary<string, int> Cloud { get; } = new Dictionary<string, int>();

        /// <summary>
        ///     Insert text(s) to create or append cloud.
        ///     Multiple textBlocks inserted at the same time will only add each word once.
        /// </summary>
        /// <param name="textBlocks">Text to use.</param>
        public async Task InsertAsync(params string[] textBlocks)
        {
            _stopWords ??= await File.ReadAllLinesAsync($@"{AssemblyLocation}\{MySQLMyISAMText}");

            foreach (var word in ExtractWords(textBlocks.CombineAll()))
                InsertWord(word);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "<Pending>")]
        private static IEnumerable<string> ExtractWords(string text)
            => text
                .ToLowerInvariant()
                .RemoveNumbers()
                .Split()
                .Select(word => word.RemoveTrailingPunctuation())
                .Select(word => word.RemoveHashFromHashTag())
                .Select(word => word.RemoveAlternateQuotationMarks())
                .Distinct();

        private void InsertWord(string word)
        {
            if (word.Length < 2) return;
            if (_stopWords!.Contains(word.Replace("’", "'", StringComparison.InvariantCultureIgnoreCase))) return;

            if (Cloud.ContainsKey(word))
                Cloud[word] += 1;
            else
                Cloud.Add(word, 1);
        }
    }
}
