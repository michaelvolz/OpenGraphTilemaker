using System.Threading.Tasks;

namespace Experiment.Features.Counter
{
    public partial class Counter
    {
        private CounterState CounterState => Store.GetState<CounterState>();

        private async Task ButtonClick() => await RequestAsync(new CounterState.IncrementCounterRequest {Amount = 2});
    }
}