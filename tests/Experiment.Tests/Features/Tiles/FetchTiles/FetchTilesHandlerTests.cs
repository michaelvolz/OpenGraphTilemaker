using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BaseTestCode;
using Experiment.Features.OpenGraphTiles;
using FluentAssertions;
using OpenGraphTilemaker.Tests;
using Xunit;
using Xunit.Abstractions;
using Xunit.Categories;

namespace Experiment.Tests.Features.OpenGraphTiles
{
    public class FetchTilesHandlerTests : IntegrationTests<FetchTilesHandlerTests>
    {
        public FetchTilesHandlerTests(ITestOutputHelper testConsole) : base(testConsole) { }

        [Fact]
        public async Task FetchTilesRequest_FakeHttpClient()
        {
            // Arrange
            var request = new TilesState.FetchTilesRequest();
            var response = "<head><meta property=\"og:title\" content=\"Microsoft launches Spend iOS app that automatically tracks and matches expenses\" />";
            response += "<meta property=\"og:image\" content=\"image\" />";
            response += "<meta property=\"og:description\" content=\"description\" /></head>";
            var mockStore = new MockStore();
            mockStore.SetState(new TilesState());
            var handler = new FetchTilesHandler(mockStore, Pocket(), TileMakerClient(response), GetPocketIOptions());

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();

            var state = mockStore.GetState<TilesState>();
            state.OriginalTiles.Should().NotBeNullOrEmpty();
            var first = state.OriginalTiles.First();
            first.Title.Should().NotBeNullOrWhiteSpace();
            first.Title.Should().Be("Microsoft launches Spend iOS app that automatically tracks and matches expenses");

            TestConsole.WriteLine(first.Title);
        }

        [Fact(Skip = "Manual testing only")]
        [IntegrationTest]
        public async Task FetchTilesRequest_RealHttpClient()
        {
            // Arrange
            var request = new TilesState.FetchTilesRequest();
            var mockStore = new MockStore();
            mockStore.SetState(new TilesState());
            var handler = new FetchTilesHandler(mockStore, Pocket(), RealTileMakerClient(), GetPocketIOptions());

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            var state = mockStore.GetState<TilesState>();
            state.OriginalTiles.Should().NotBeNullOrEmpty();

            foreach (var tile in state.OriginalTiles) TestConsole.WriteLine(tile?.Title ?? "---");
        }
    }
}