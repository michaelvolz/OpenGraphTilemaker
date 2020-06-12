using System.Collections.Generic;
using BlazorState;
using Common;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.OpenGraphTiles
{
    public partial class TilesState
    {
        public class SortTilesRequest : IAction
        {
            public List<OpenGraphMetadata>? CurrentTiles { get; set; }

            public string SortProperty { get; set; } = string.Empty;
            public SortOrder SortOrder { get; set; } = SortOrder.Undefined;
        }
    }
}
