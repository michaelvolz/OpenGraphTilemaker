#nullable enable
using System.Collections.Generic;
using System.Linq;
using Common;
using OpenGraphTilemaker.OpenGraph;

// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Experiment.Features.Tiles
{
    public partial class TilesState
    {
        public List<OpenGraphMetadata> CurrentTiles { get; private set; } = new List<OpenGraphMetadata>();
        public IOrderedEnumerable<KeyValuePair<string, int>>? TagCloud { get; private set; }

        public string SortProperty { get; private set; } = string.Empty;
        public SortOrder SortOrder { get; private set; } = SortOrder.Ascending;

        public string SearchText { get; set; } = string.Empty;
        public string LastSearchText { get; private set; } = string.Empty;
    }
}