using MediatR;

namespace OpenGraphTilemaker.Web.Client.Features.Counter
{
    public class IncrementCounterRequest : IRequest<CounterState>
    {
        public int Amount { get; set; }
    }
}