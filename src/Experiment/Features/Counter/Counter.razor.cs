using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Common;
using Experiment.Features.App;

namespace Experiment.Features.Counter
{
    [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
    public class CounterModel : BlazorComponentStateful<CounterModel>
    {
        public CounterState CounterState => Store.GetState<CounterState>();

        [BlazorEvent]
        public async Task ButtonClick() => await RequestAsync(new CounterState.IncrementCounterRequest {Amount = 2});
    }
}