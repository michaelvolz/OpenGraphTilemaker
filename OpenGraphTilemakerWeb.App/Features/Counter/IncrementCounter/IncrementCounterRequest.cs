using MediatR;

namespace OpenGraphTilemakerWeb.App.Features.Counter
{
    public class IncrementCounterRequest : IRequest<CounterState>
    {
        public int Amount { get; set; }
    }
}