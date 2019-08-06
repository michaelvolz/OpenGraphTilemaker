using System;
using System.Linq;
using System.Threading.Tasks;
using BaseTestCode;
using BaseTestCode.XUnitUtilities;
using Common;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using OpenGraphTilemaker.GetPocket;
using Xunit;
using Xunit.Abstractions;
using Xunit.Categories;

namespace OpenGraphTilemaker.Tests.GetPocket
{
    public class PocketTests : BaseTest<PocketTests>, IDisposable
    {
        public PocketTests(ITestOutputHelper testConsole) : base(testConsole)
        {
            // Arrange
            var feedService = new Feed<PocketEntry>();
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _pocket = new Pocket(_memoryCache, feedService);
            _options = new PocketOptions(Uri, CachingTimeSpan, TimeoutTimeSpan);
        }

        public void Dispose() => _memoryCache?.Dispose();

        private static readonly Uri Uri = new Uri("https://getpocket.com/users/Flynn0r/feed/all");
        private static readonly TimeSpan CachingTimeSpan = TimeSpan.FromSeconds(1);
        private static readonly TimeSpan TimeoutTimeSpan = TimeSpan.FromSeconds(15);

        private readonly Pocket _pocket;
        private readonly PocketOptions _options;
        private readonly MemoryCache _memoryCache;

        [Fact]
        [IntegrationTest]
        public async Task GetEntries()
        {
            // Act
            var entries = await _pocket.GetEntriesAsync(_options);

            // Assert
            entries.Should().NotBeNullOrEmpty();

            var first = entries.First();
            first.Title.Should().NotBeNullOrWhiteSpace();
            first.Link.Should().NotBeNull();
            first.PubDate.Should().NotBeSameDateAs(default);

            // Log
            foreach (var item in entries)
            {
                TestConsole.WriteLine(item.Title);
                TestConsole.WriteLine(item.Category);
                TestConsole.WriteLine(item.Link);
                TestConsole.WriteLine(item.PubDate.ToShortDateString());
                TestConsole.WriteLine(string.Empty);
            }
        }
    }
}