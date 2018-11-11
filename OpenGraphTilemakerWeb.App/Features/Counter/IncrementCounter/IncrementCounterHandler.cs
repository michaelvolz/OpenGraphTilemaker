using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using Common;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemakerWeb.App.Features.Counter
{
    public partial class CounterState
    {
        [IoC]
        public class IncrementCounterHandler : RequestHandler<IncrementCounterRequest, CounterState>
        {
            public IncrementCounterHandler(IStore store) : base(store) { }

            public CounterState CounterState => Store.GetState<CounterState>();

            public override Task<CounterState> Handle(IncrementCounterRequest req, CancellationToken token) {
                CounterState.Count += req.Amount;
                return Task.FromResult(CounterState);
            }
        }
    }
}