using System.Collections.Generic;
using BlazorState;
using Domain.OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.OpenGraphTiles
{
    public partial class TilesState
    {
        public class SearchTilesRequest : IAction
        {
            public List<OpenGraphMetadata> OriginalTiles { get; set; } = new List<OpenGraphMetadata>();

            public string SearchText { get; set; } = string.Empty;
        }
    }
}
