using System.Collections.Generic;
using BlazorState;
using Common;
using MediatR;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.Tiles
{
    public class SortTilesRequest : IAction
    {
        public List<OpenGraphMetadata>? CurrentTiles { get; set; }

        public string SortProperty { get; set; } = string.Empty;
        public SortOrder SortOrder { get; set; } = SortOrder.Undefined;
    }
}