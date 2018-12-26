using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.Extensions;
using static Common.Extensions.AssemblyExtensions;

namespace Common.TagCloud
{
    public class TagCloud
    {
        private const string MySQLMyISAMText = @"TagCloud\mysql_myisam.txt";
        private const string Space = " ";
        private static string[] _stopWords;

        public Dictionary<string, int> Cloud { get; } = new Dictionary<string, int>();

        private static string CombineTexts(string[] texts) => texts.Aggregate(string.Empty, (current, next) => $"{current}{Space}{next}");

        private static IEnumerable<string> ExtractWords(string text) => text.ToLowerInvariant().RemoveNumbers().Split().Distinct();

        private static string NormalizeWord(string word) => word.TrimEnd('.').Replace("’", "'");

        /// <summary>
        ///     Multiple texts inserted at the same time will only add each word once.
        /// </summary>
        /// <param name="texts">Text to use.</param>
        /// <returns>
        ///     <see cref="Task" />
        /// </returns>
        public async Task InsertAsync(params string[] texts) {
            await Task.FromResult(0); // async placeholder, File.ReadAllLines has no async in netstandard2.0

            if (_stopWords == null) _stopWords = File.ReadAllLines($@"{AssemblyLocation}\{MySQLMyISAMText}");

            foreach (var word in ExtractWords(CombineTexts(texts)))
                InsertWord(word);
        }

        private void InsertWord(string word) {
            var normalizedWord = NormalizeWord(word);

            if (normalizedWord.Length < 2) return;
            if (_stopWords.Contains(normalizedWord, StringComparer.InvariantCultureIgnoreCase)) return;

            if (Cloud.ContainsKey(normalizedWord))
                Cloud[normalizedWord] += 1;
            else
                Cloud.Add(normalizedWord, 1);
        }
    }
}
