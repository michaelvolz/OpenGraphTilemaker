using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using BlazorState;
using Common;
using MediatR;

namespace Experiment.Features.CounterControl
{
    public partial class CounterState
    {
        [IoC]
        public class IncrementCounterHandler : ActionHandler<IncrementCounterRequest>
        {
            public IncrementCounterHandler(IStore store)
                : base(store)
            {
            }

            private CounterState CounterState => Store.GetState<CounterState>();

            public override Task<Unit> Handle(IncrementCounterRequest aAction, CancellationToken aCancellationToken)
            {
                Guard.Against.Null(aAction, nameof(aAction));

                CounterState.Count += aAction.Amount;

                return Unit.Task;
            }
        }
    }
}
