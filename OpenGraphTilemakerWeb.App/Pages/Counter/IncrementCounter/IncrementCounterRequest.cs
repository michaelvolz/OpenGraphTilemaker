using MediatR;

namespace OpenGraphTilemakerWeb.App.Pages.Counter
{
    public class IncrementCounterRequest : IRequest<CounterState>
    {
        public int Amount { get; set; }
    }
}