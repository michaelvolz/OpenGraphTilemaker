using System.Threading.Tasks;
using Common;
using Experiment.Features.App;

// ReSharper disable MemberCanBeProtected.Global

namespace Experiment.Features.Counter
{
    public class CounterModel : BlazorComponentStateful<CounterModel>
    {
        public CounterState CounterState => Store.GetState<CounterState>();

        [BlazorEvent]
        public async Task ButtonClick() => await RequestAsync(new CounterState.IncrementCounterRequest {Amount = 2});
    }
}