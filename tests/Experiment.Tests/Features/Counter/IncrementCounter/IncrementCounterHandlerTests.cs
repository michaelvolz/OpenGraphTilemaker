using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using OpenGraphTilemaker.Tests;
using Experiment.Features.Counter;
using Xunit;
using Xunit.Abstractions;

namespace Experiment.Tests.Features.Counter
{
    public class IncrementCounterHandlerTests : IntegrationTests<IncrementCounterHandlerTests>
    {
        public IncrementCounterHandlerTests(ITestOutputHelper testConsole) : base(testConsole) { }

        [Fact]
        public async Task IncrementCounterRequest_AmountDefined()
        {
            // Arrange
            var amount = 7;
            var request = new CounterState.IncrementCounterRequest { Amount = amount };
            var mockStore = new MockStore();
            mockStore.SetState(new CounterState());
            var handler = new CounterState.IncrementCounterHandler(mockStore);
            var initialCount = mockStore.GetState<CounterState>().Count;

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            var state = mockStore.GetState<CounterState>();

            state.Count.Should().Be(initialCount + amount);
        }
    }
}
