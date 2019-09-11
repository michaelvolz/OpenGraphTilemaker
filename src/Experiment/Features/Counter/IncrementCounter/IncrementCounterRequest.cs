using BlazorState;

namespace Experiment.Features.Counter
{
    public class IncrementCounterRequest : IAction
    {
        public int Amount { get; set; }
    }
}