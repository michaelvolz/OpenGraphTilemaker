// ReSharper disable MemberCanBeProtected.Global

namespace OpenGraphTilemaker.Web.Client.Features.Counter
{
    public class CounterModel : BlazorComponentStateful
    {
        public CounterState CounterState => Store.GetState<CounterState>();

        public void ButtonClick() {
            Request(new IncrementCounterRequest {Amount = 2});
        }
    }
}