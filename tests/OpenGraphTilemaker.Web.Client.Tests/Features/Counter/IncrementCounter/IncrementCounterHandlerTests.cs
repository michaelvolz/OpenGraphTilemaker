//using System.Threading;
//using System.Threading.Tasks;
//using FluentAssertions;
//using OpenGraphTilemaker.Tests;
//using OpenGraphTilemaker.Web.Client.Features.Counter;
//using Xunit;
//using Xunit.Abstractions;

//namespace OpenGraphTilemaker.Web.Client.Tests.Features.Counter
//{
//    public class IncrementCounterHandlerTests : ClientBaseTest<IncrementCounterHandlerTests>
//    {
//        public IncrementCounterHandlerTests(ITestOutputHelper testConsole) : base(testConsole) {
//            // Arrange
//            var mockStore = new MockStore();
//            mockStore.SetState(new CounterState());
//            _handler = new CounterState.IncrementCounterHandler(mockStore);
//        }

//        private readonly CounterState.IncrementCounterHandler _handler;

//        [Fact]
//        public async Task IncrementCounterRequest() {
//            // Arrange
//            var amount = 7;
//            var request = new IncrementCounterRequest { Amount = amount };

//            // Act
//            var result = await _handler.Handle(request, CancellationToken.None);

//            // Assert
//            result.Should().NotBeNull();
//            result.Count.Should().Be(3 + amount);
//        }
//    }
//}
