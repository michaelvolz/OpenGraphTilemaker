using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BlazorState;
using Common;
using Domain.OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.OpenGraphTilesControl
{
    public partial class TilesState
    {
        public class SortTilesRequest : IAction
        {
            [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Unnecessary")]
            public IList<OpenGraphMetadata>? CurrentTiles { get; init; }

            public string SortProperty { get; init; } = string.Empty;
            public SortOrder SortOrder { get; init; } = SortOrder.Undefined;
        }
    }
}
