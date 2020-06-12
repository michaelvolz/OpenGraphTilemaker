using System.Diagnostics.CodeAnalysis;

namespace Experiment.Features.Counter
{
    [SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Needed for Blazor.State")]
    public partial class CounterState
    {
        public int Count { get; private set; }
    }
}
