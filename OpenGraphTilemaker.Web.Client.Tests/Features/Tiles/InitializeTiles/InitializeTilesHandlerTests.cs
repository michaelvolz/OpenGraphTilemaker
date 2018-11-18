using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using OpenGraphTilemaker.Tests;
using OpenGraphTilemaker.Web.Client.Features.Tiles;
using Xunit;
using Xunit.Abstractions;

namespace OpenGraphTilemaker.Web.Client.Tests.Features.Tiles
{
    public class InitializeTilesHandlerTests : ClientBaseTest
    {
        public InitializeTilesHandlerTests(ITestOutputHelper testConsole) : base(testConsole) { }

        [Fact]
        public async Task InitializeTilesRequest_WithRealHttpClient() {
            // Arrange
            var request = new InitializeTilesRequest();
            var handler = new TilesState.InitializeTilesHandler(Pocket(), RealTileMakerClient(), GetPocketIOptions(), MockStore(new TilesState()));

            // Act
            var result = await handler.Handle(request, new CancellationToken());

            // Assert
            result.Should().NotBeNull();
            result.Tiles.Should().NotBeNullOrEmpty();

            foreach (var tile in result.Tiles) {
                TestConsole.WriteLine(tile?.Title ?? "---");
            }
        }

        [Fact]
        public async Task InitializeTilesRequest() {
            // Arrange
            var request = new InitializeTilesRequest();
            var response = "<head><meta property=\"og:title\" content=\"Microsoft launches Spend iOS app that automatically tracks and matches expenses\" />";
            response += "<meta property=\"og:image\" content=\"image\" />";
            response += "<meta property=\"og:description\" content=\"description\" /></head>";
            var handler = new TilesState.InitializeTilesHandler(Pocket(), TileMakerClient(response), GetPocketIOptions(), MockStore(new TilesState()));

            // Act
            var result = await handler.Handle(request, new CancellationToken());

            // Assert
            result.Should().NotBeNull();
            result.Tiles.Should().NotBeNullOrEmpty();
            var first = result.Tiles.First();
            first.Title.Should().NotBeNullOrWhiteSpace();
            first.Title.Should().Be("Microsoft launches Spend iOS app that automatically tracks and matches expenses");
            
            TestConsole.WriteLine(first.Title);
        }
    }
}