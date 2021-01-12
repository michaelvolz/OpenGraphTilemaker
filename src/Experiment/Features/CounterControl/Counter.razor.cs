using System.Threading.Tasks;

namespace Experiment.Features.CounterControl
{
    public partial class Counter
    {
        private CounterState CounterState => Store.GetState<CounterState>();

        private async Task ButtonClick() => await RequestAsync(new CounterState.IncrementCounterRequest { Amount = 2 });
    }
}
