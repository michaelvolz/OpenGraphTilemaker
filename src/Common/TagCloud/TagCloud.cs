using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Common.Extensions;
using static Common.Extensions.AssemblyExtensions;

// ReSharper disable InconsistentNaming

namespace Common.TagCloud
{
    public class TagCloud
    {
        private const string MySQL_MyISAM_Text = @"TagCloud\mysql_myisam.txt";
        private static string[]? _stopWords;

        public Dictionary<string, int> Cloud { get; } = new Dictionary<string, int>();

        private static IEnumerable<string> ExtractWords(string text)
            => text
                .ToLowerInvariant()
                .RemoveNumbers()
                .Split()
                .Select(word => word.RemoveTrailingPunctuation())
                .Select(word => word.RemoveHashFromHashTag())
                .Select(word => word.RemoveAlternateQuotationMarks())
                .Distinct();

        /// <summary>
        ///     Insert text(s) to create or append cloud.
        ///     Multiple textBlocks inserted at the same time will only add each word once.
        /// </summary>
        /// <param name="textBlocks">Text to use.</param>
        public async Task InsertAsync(params string[] textBlocks)
        {
            await Task.FromResult(0); // async placeholder, File.ReadAllLines has no async in netstandard2.0

            _stopWords ??= File.ReadAllLines($@"{AssemblyLocation}\{MySQL_MyISAM_Text}");

            foreach (var word in ExtractWords(textBlocks.CombineAll()))
                InsertWord(word);
        }

        private void InsertWord(string word)
        {
            if (word.Length < 2) return;
            if (_stopWords.Contains(word.Replace("’", "'"), StringComparer.InvariantCultureIgnoreCase)) return;

            if (Cloud.ContainsKey(word))
                Cloud[word] += 1;
            else
                Cloud.Add(word, 1);
        }
    }
}