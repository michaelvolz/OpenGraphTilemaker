using BlazorState;

namespace Experiment.Features.CounterControl
{
    public partial class CounterState
    {
        public class IncrementCounterRequest : IAction
        {
            public int Amount { get; init; }
        }
    }
}
