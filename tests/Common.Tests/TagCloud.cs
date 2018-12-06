using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Microsoft.Extensions.Logging;

namespace Common.Tests
{
    public class TagCloud
    {
        private string[] _stopWords;
        public Dictionary<string, int> Cloud { get; } = new Dictionary<string, int>();

        public async Task InsertAsync(string text) {
            if (_stopWords == null) _stopWords = await File.ReadAllLinesAsync("../../../mysql_myisam.txt");

            var textOnly = text
                .Where(c => !char.IsPunctuation(c))
                .Aggregate(new StringBuilder(), (current, next) => current.Append(next), sb => sb.ToString());

            var words = textOnly.Split();

            foreach (var word in words) {
                var logger = ApplicationLogging.CreateLogger<TagCloud>();
                logger.LogInformation($"Word: {word}");

                var upperCaseWord = word.ToUpperInvariant();
                if (_stopWords.Contains(upperCaseWord, StringComparer.InvariantCultureIgnoreCase)) continue;

                if (Cloud.ContainsKey(upperCaseWord))
                    Cloud[upperCaseWord] += 1;
                else
                    Cloud.Add(upperCaseWord, 1);
            }
        }
    }
}
