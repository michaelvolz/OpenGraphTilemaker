using System.Collections.Generic;
using Common;
using OpenGraphTilemaker.OpenGraph;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public partial class TilesState
    {
        public List<OpenGraphMetadata> CurrentTiles { get; private set; }

        public string SortProperty { get; private set; }
        public SortOrder SortOrder { get; private set; }

        public string SearchText { get; set; }
        public string LastSearchText { get; private set; }
    }
}
