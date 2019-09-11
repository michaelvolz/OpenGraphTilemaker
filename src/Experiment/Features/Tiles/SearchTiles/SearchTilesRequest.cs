using System.Collections.Generic;
using BlazorState;
using MediatR;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.Tiles
{
    public class SearchTilesRequest : IAction
    {
        public List<OpenGraphMetadata> OriginalTiles { get; set; } = new List<OpenGraphMetadata>();

        public string SearchText { get; set; } = string.Empty;
    }
}
