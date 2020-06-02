using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using Common;
using MediatR;

namespace Experiment.Features.Counter
{
    public partial class CounterState
    {
        [IoC]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public class IncrementCounterHandler : ActionHandler<IncrementCounterRequest>
        {
            public IncrementCounterHandler(IStore store) : base(store) { }

            public CounterState CounterState => Store.GetState<CounterState>();

            public override Task<Unit> Handle(IncrementCounterRequest req, CancellationToken token)
            {
                CounterState.Count += req.Amount;

                return Unit.Task;
            }
        }
    }
}