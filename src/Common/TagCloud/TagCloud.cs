using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Common.Extensions;

namespace Common.TagCloud
{
    public class TagCloud
    {
        private const string MySQLMyISAMText = @"TagCloud\mysql_myisam.txt";
        private static string[] _stopWords;
        private readonly string _location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public Dictionary<string, int> Cloud { get; } = new Dictionary<string, int>();

        private static string NormalizedWord(string word) => word.TrimEnd('.').Replace("’", "'");

        public async Task InsertAsync(string text) {
            await Task.FromResult(0); // async placeholder

            if (_stopWords == null) _stopWords = File.ReadAllLines($@"{_location}\{MySQLMyISAMText}");

            foreach (var word in text.RemoveNumbers().Split())
                InsertWord(word);
        }

        private void InsertWord(string word) {
            var normalizedWord = NormalizedWord(word);

            if (normalizedWord.Length < 2) return;
            if (_stopWords.Contains(normalizedWord, StringComparer.InvariantCultureIgnoreCase)) return;

            var lowerCaseWord = normalizedWord.ToLowerInvariant();
            if (Cloud.ContainsKey(lowerCaseWord))
                Cloud[lowerCaseWord] += 1;
            else
                Cloud.Add(lowerCaseWord, 1);
        }
    }
}
