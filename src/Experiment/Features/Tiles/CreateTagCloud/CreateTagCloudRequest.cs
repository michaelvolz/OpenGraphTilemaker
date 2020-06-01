using System.Collections.Generic;
using BlazorState;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.Tiles
{
    public partial class TilesState
    {
        public class CreateTagCloudRequest : IAction
        {
            public List<OpenGraphMetadata> OriginalTiles { get; set; } = new List<OpenGraphMetadata>();
        }
    }
}