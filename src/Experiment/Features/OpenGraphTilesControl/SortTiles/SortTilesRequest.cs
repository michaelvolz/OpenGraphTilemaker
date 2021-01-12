using System.Collections.Generic;
using BlazorState;
using Common;
using Domain.OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.OpenGraphTilesControl
{
    public partial class TilesState
    {
        public class SortTilesRequest : IAction
        {
            public IList<OpenGraphMetadata>? CurrentTiles { get; set; }

            public string SortProperty { get; set; } = string.Empty;
            public SortOrder SortOrder { get; set; } = SortOrder.Undefined;
        }
    }
}
