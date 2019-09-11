using BlazorState;

// ReSharper disable once CheckNamespace
namespace Sample.Client.Features.Counter
{
    public partial class CounterState : State<CounterState>
    {
        public int Count { get; private set; }
        protected override void Initialize() => Count = 3;
    }
}