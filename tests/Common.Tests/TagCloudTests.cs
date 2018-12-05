using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseTestCode;
using Common.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace Common.Tests
{
    public class TagCloudTestsTests : BaseTest<TagCloudTestsTests>
    {
        public TagCloudTestsTests(ITestOutputHelper testConsole) : base(testConsole) { }

        [Fact]
        public async Task TagCloud_InsertText_ReturnsValidCloud() {
            var tagCloud = new TagCloud();

            var text = "Earthly of he parasites at so and for call shrine of old pomp to could that fondly one did hight Earthly";
            await tagCloud.InsertAsync(text);

            var cloud = tagCloud.Cloud;

            cloud.Should().NotBeNull();
            cloud.Should().ContainKey("Earthly".ToUpperInvariant());
            cloud.Should().NotContainKey("of".ToUpperInvariant());
            cloud.Should().NotContainKey("he".ToUpperInvariant());
            cloud.Should().ContainKey("parasites".ToUpperInvariant());
            cloud.Should().NotContainKey("at".ToUpperInvariant());
            cloud.Should().NotContainKey("so".ToUpperInvariant());
            cloud.Should().NotContainKey("and".ToUpperInvariant());
            cloud.Should().NotContainKey("for".ToUpperInvariant());
            cloud.Should().ContainKey("call".ToUpperInvariant());
            cloud.Should().ContainKey("shrine".ToUpperInvariant());
            cloud.Should().NotContainKey("of".ToUpperInvariant());
            cloud.Should().NotContainKey("old".ToUpperInvariant());
            cloud.Should().ContainKey("pomp".ToUpperInvariant());
            cloud.Should().NotContainKey("old".ToUpperInvariant());
            cloud.Should().NotContainKey("could".ToUpperInvariant());
            cloud.Should().NotContainKey("that".ToUpperInvariant());
            cloud.Should().ContainKey("fondly".ToUpperInvariant());
            cloud.Should().NotContainKey("one".ToUpperInvariant());
            cloud.Should().NotContainKey("did".ToUpperInvariant());
            cloud.Should().ContainKey("hight".ToUpperInvariant());

            var (key, value) = cloud.First();
            key.Should().BeEquivalentTo("earthly");
            value.Should().Be(2);

            TestConsole.WriteLine("{@cloud}", cloud);
        }
    }

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
