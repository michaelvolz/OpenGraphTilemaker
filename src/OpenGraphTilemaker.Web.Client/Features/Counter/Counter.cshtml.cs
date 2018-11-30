// ReSharper disable MemberCanBeProtected.Global

using System.Threading.Tasks;

namespace OpenGraphTilemaker.Web.Client.Features.Counter
{
    public class CounterModel : BlazorComponentStateful<CounterModel>
    {
        public CounterState CounterState => Store.GetState<CounterState>();

        public async Task ButtonClickAsync() {
            await RequestAsync(new IncrementCounterRequest { Amount = 2 });
        }
    }
}
