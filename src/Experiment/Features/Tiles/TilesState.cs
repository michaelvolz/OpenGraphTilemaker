using System.Collections.Generic;
using Common;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.Tiles
{
    public partial class TilesState
    {
        public List<OpenGraphMetadata> FilteredAndSortedTiles { get; private set; } = new List<OpenGraphMetadata>();
        public List<OpenGraphMetadata> OriginalTiles { get; set; } = new List<OpenGraphMetadata>();

        public Dictionary<string, int>? TagCloud { get; private set; }

        public string SortProperty { get; private set; } = string.Empty;
        public SortOrder SortOrder { get; private set; } = SortOrder.Undefined;

        public string SearchText { get; private set; } = string.Empty;
    }
}