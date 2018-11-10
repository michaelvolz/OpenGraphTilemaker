using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using Common;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemakerWeb.App.Pages.Counter
{
    public partial class CounterState
    {
        [IoC]
        public class IncrementCounterHandler : RequestHandler<IncrementCounterRequest, CounterState>
        {
            public IncrementCounterHandler(IStore store) : base(store)
            {
            }

            public CounterState CounterState => Store.GetState<CounterState>();

            public override Task<CounterState> Handle(IncrementCounterRequest incrementCounterRequest,
                CancellationToken cancellationToken)
            {
                CounterState.Count += incrementCounterRequest.Amount;
                return Task.FromResult(CounterState);
            }
        }
    }
}