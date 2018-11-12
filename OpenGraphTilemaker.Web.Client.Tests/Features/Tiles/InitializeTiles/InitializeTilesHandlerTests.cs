using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using OpenGraphTilemaker.Web.Client.Features.Tiles;
using Xunit;
using Xunit.Abstractions;

namespace OpenGraphTilemaker.Web.Client.Tests.Features.Tiles
{
    public class InitializeTilesHandlerTests : ClientBaseTest
    {
        public InitializeTilesHandlerTests(ITestOutputHelper testConsole) : base(testConsole) { }

        [Fact]
        public async Task InitializeTilesRequest() {
            // Arrange
            var request = new InitializeTilesRequest();
            var handler = new TilesState.InitializeTilesHandler(Pocket(), TileMakerClient(), GetPocketIOptions(), MockStore(new TilesState()));

            // Act
            var result = await handler.Handle(request, new CancellationToken());

            // Assert
            result.Should().NotBeNull();
            result.Tiles.Should().NotBeNullOrEmpty();
        }
    }
}