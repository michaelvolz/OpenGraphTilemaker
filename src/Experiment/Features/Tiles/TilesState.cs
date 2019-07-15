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
        public List<OpenGraphMetadata> CurrentTiles { get; private set; }
        //public IOrderedEnumerable<KeyValuePair<string, int>> TagCloud { get; private set; }

        public string SortProperty { get; private set; }
        public SortOrder SortOrder { get; private set; }

        public string SearchText { get; set; }
        public string LastSearchText { get; private set; }
    }
}
