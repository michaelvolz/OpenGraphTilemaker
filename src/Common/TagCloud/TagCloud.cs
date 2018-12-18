using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Logging;
using Microsoft.Extensions.Logging;

#pragma warning disable CA1308 // Normalize strings to uppercase

namespace Common.TagCloud
{
    public class TagCloud
    {
        private const string MySQLMyISAMText = @"TagCloud\mysql_myisam.txt";
        private static string[] _stopWords;
        private readonly string _location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public Dictionary<string, int> Cloud { get; } = new Dictionary<string, int>();

        public async Task InsertAsync(string text) {
            await Task.FromResult(0);

            if (_stopWords == null) _stopWords = File.ReadAllLines($@"{_location}\{MySQLMyISAMText}");

            var words = text.RemoveNumbers().Split();

            foreach (var word in words) {
                var logger = ApplicationLogging.CreateLogger<TagCloud>();
                // logger.LogInformation($"Word: {word}");

                var normalizedWord = word.TrimEnd('.').Replace("’", "'");

                if (normalizedWord.Length < 2) continue;

                if (_stopWords.Contains(normalizedWord, StringComparer.InvariantCultureIgnoreCase)) continue;

                var lowerCaseWord = normalizedWord.ToLowerInvariant();
                if (Cloud.ContainsKey(lowerCaseWord))
                    Cloud[lowerCaseWord] += 1;
                else
                    Cloud.Add(lowerCaseWord, 1);
            }
        }
    }
}
