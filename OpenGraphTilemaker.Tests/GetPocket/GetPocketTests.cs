using System;
using System.Linq;
using System.Threading.Tasks;
using Common;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using OpenGraphTilemaker.GetPocket;
using Xunit;
using Xunit.Abstractions;

namespace OpenGraphTilemaker.Tests.GetPocket
{
    public class GetPocketTests : BaseTest
    {
        public GetPocketTests(ITestOutputHelper testConsole) : base(testConsole) {
            // Arrange
            var feedService = new FeedService<GetPocketEntry>();
            _pocket = new OpenGraphTilemaker.GetPocket.GetPocket(new MemoryCache(new MemoryCacheOptions()), feedService);
            _options = new GetPocketOptions(Uri, CachingTimeSpan);
        }

        private static readonly Uri Uri = new Uri("https://getpocket.com/users/Flynn0r/feed/all");
        private static readonly TimeSpan CachingTimeSpan = TimeSpan.FromSeconds(1);

        private readonly OpenGraphTilemaker.GetPocket.GetPocket _pocket;
        private readonly GetPocketOptions _options;

        [Fact]
        public async Task GetUrls_Scenario_Behavior() {
            // Act
            var entries = await _pocket.GetEntriesAsync(_options);

            // Assert
            entries.Should().NotBeNullOrEmpty();

            var first = entries.First();
            first.Title.Should().NotBeNullOrWhiteSpace();
            first.Link.Should().NotBeNull();
            first.PubDate.Should().NotBeSameDateAs(default);
        }

        [Fact]
        public async Task Method_Scenario_Behavior() {
            // Act
            var entries = await _pocket.GetEntriesAsync(_options);

            // Log
            foreach (var item in entries) {
                TestConsole.WriteLine(item.Title);
                TestConsole.WriteLine(item.Category);
                TestConsole.WriteLine(item.Link);
                TestConsole.WriteLine(item.PubDate.ToShortDateString());
                TestConsole.WriteLine("");
            }

            // Assert
            entries.Should().NotBeNull();
        }
    }
}