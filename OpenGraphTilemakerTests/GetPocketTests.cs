using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using OpenGraphTilemaker;
using Xunit;
using Xunit.Abstractions;

namespace OpenGraphTilemakerTests
{
    public class GetPocketTests : BaseTest
    {
        public GetPocketTests(ITestOutputHelper testConsole) : base(testConsole)
        {
        }

        [Fact]
        public async Task GetUrls_Scenario_Behavior()
        {
            var pocket = new GetPocket(new MemoryCache(new MemoryCacheOptions()));

            var urls = await pocket.GetEntriesAsync(new Uri("https://getpocket.com/users/Flynn0r/feed/all"),
                TimeSpan.FromSeconds(0));

            urls.Should().NotBeNullOrEmpty();
            var first = urls.First();
            first.Title.Should().NotBeNullOrWhiteSpace();
            first.Link.Should().NotBeNull();
            first.PubDate.Should().NotBeSameDateAs(default);
        }

        [Fact]
        public async Task Method_Scenario_Behavior()
        {
            var pocket = new GetPocket(new MemoryCache(new MemoryCacheOptions()));

            var rss = await pocket.GetEntriesAsync(new Uri("https://getpocket.com/users/Flynn0r/feed/all"),
                TimeSpan.FromSeconds(0));

            foreach (var item in rss)
            {
                TestConsole.WriteLine(item.Title);
                TestConsole.WriteLine(item.Category);
                TestConsole.WriteLine(item.Link);
                TestConsole.WriteLine(item.PubDate.ToShortDateString());
                TestConsole.WriteLine("");
            }

            rss.Should().NotBeNull();
        }
    }
}