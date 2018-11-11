// ReSharper disable MemberCanBeProtected.Global

namespace OpenGraphTilemakerWeb.App.Features.Counter
{
    public class CounterModel : BlazorComponentStateful
    {
        public CounterState CounterState => Store.GetState<CounterState>();

        public void ButtonClick() {
            Request(new IncrementCounterRequest {Amount = 2});
        }
    }
}