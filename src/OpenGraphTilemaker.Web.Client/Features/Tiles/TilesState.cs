using System.Collections.Generic;
using Common;
using OpenGraphTilemaker.OpenGraph;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public partial class TilesState
    {
        public string SortProperty { get; private set; }
        public SortOrder SortOrder { get; private set; }
        public List<OpenGraphMetadata> Tiles { get; private set; }
    }
}
