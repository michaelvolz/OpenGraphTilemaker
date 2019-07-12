//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using BaseTestCode;
//using FluentAssertions;
//using OpenGraphTilemaker.Tests;
//using OpenGraphTilemaker.Web.Client.Features.Tiles;
//using Xunit;
//using Xunit.Abstractions;

//namespace OpenGraphTilemaker.Web.Client.Tests.Features.Tiles
//{
//    public class FetchTilesHandlerTests : ClientBaseTest<FetchTilesHandlerTests>
//    {
//        public FetchTilesHandlerTests(ITestOutputHelper testConsole) : base(testConsole) { }

//        //[Fact]
//        public async Task FetchTilesRequest_FakeHttpClient() {
//            // Arrange
//            var request = new FetchTilesRequest();
//            var response = "<head><meta property=\"og:title\" content=\"Microsoft launches Spend iOS app that automatically tracks and matches expenses\" />";
//            response += "<meta property=\"og:image\" content=\"image\" />";
//            response += "<meta property=\"og:description\" content=\"description\" /></head>";
//            var handler = new FetchTilesHandler(Pocket(), TileMakerClient(response), GetPocketIOptions());

//            // Act
//            var result = await handler.Handle(request, CancellationToken.None);

//            // Assert
//            result.Should().NotBeNull();
//            result.OriginalTiles.Should().NotBeNullOrEmpty();
//            var first = result.OriginalTiles.First();
//            first.Title.Should().NotBeNullOrWhiteSpace();
//            first.Title.Should().Be("Microsoft launches Spend iOS app that automatically tracks and matches expenses");

//            TestConsole.WriteLine(first.Title);
//        }

//        //[Fact]
//        //[XunitCategory("Integration")]
//        public async Task FetchTilesRequest_RealHttpClient() {
//            // Arrange
//            var request = new FetchTilesRequest();
//            var handler = new FetchTilesHandler(Pocket(), RealTileMakerClient(), GetPocketIOptions());

//            // Act
//            var result = await handler.Handle(request, CancellationToken.None);

//            // Assert
//            result.Should().NotBeNull();
//            result.OriginalTiles.Should().NotBeNullOrEmpty();

//            foreach (var tile in result.OriginalTiles) TestConsole.WriteLine(tile?.Title ?? "---");
//        }
//    }
//}
