/*using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using MediatR;

// ReSharper disable once CheckNamespace
namespace Sample.Client.Features.Counter
{
    public partial class CounterState
    {
        public class IncrementCountHandler : ActionHandler<IncrementCountAction>
        {
            public IncrementCountHandler(IStore aStore) : base(aStore) { }

            private CounterState CounterState => Store.GetState<CounterState>();

            public override Task<Unit> Handle(IncrementCountAction aIncrementCountAction, CancellationToken aCancellationToken)
            {
                CounterState.Count = CounterState.Count + aIncrementCountAction.Amount;

                return Unit.Task;
            }
        }
    }
}*/
