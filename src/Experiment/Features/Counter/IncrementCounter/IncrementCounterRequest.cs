using MediatR;

namespace Experiment.Features.Counter
{
    public class IncrementCounterRequest : IRequest<CounterState>
    {
        public int Amount { get; set; }
    }
}
