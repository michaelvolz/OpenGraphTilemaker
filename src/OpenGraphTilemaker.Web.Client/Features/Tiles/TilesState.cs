using System.Collections.Generic;
using Common;
using OpenGraphTilemaker.OpenGraph;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public partial class TilesState
    {
        //public List<OpenGraphMetadata> OriginalTiles { get; private set; }
        public List<OpenGraphMetadata> CurrentTiles { get; private set; }

        public string SortProperty { get; private set; }
        public SortOrder SortOrder { get; private set; }

        public string SearchText { get; set; }
        public string LastSearchText { get; private set; }
    }
}
