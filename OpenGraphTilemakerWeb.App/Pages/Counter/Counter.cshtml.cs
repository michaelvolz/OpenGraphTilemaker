using OpenGraphTilemakerWeb.App.ClientApp;

// ReSharper disable MemberCanBeProtected.Global

namespace OpenGraphTilemakerWeb.App.Pages.Counter
{
    public class CounterModel : BlazorComponentStateful
    {
        public CounterState CounterState => Store.GetState<CounterState>();

        public void ButtonClick()
        {
            Request(new IncrementCounterRequest {Amount = 2});
        }
    }
}