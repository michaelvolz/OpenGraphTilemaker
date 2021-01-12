using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BlazorState;
using Domain.OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.OpenGraphTilesControl
{
    public partial class TilesState
    {
        public class SearchTilesRequest : IAction
        {
            [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Unnecessary")]
            public IList<OpenGraphMetadata> OriginalTiles { get; init; } = new List<OpenGraphMetadata>();

            public string SearchText { get; init; } = string.Empty;
        }
    }
}
