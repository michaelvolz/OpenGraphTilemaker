using BlazorState;

namespace Experiment.Features.Counter
{
    public partial class CounterState
    {
        public class IncrementCounterRequest : IAction
        {
            public int Amount { get; set; }
        }
    }
}
