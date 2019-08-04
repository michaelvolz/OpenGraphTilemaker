using System.Collections.Generic;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.Tiles
{
    public class FetchTilesResponse
    {
        public List<OpenGraphMetadata> OriginalTiles { get; set; } = new List<OpenGraphMetadata>();
    }
}
