#nullable enable
using System.Collections.Generic;
using Common;
using MediatR;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.Tiles
{
    public class SortTilesRequest : IRequest<TilesState>
    {
        public List<OpenGraphMetadata>? CurrentTiles { get; set; }

        public string SortProperty { get; set; } = string.Empty;
        public SortOrder SortOrder { get; set; } = SortOrder.Undefined;
    }
}