using System.Collections.Generic;
using Common;
using OpenGraphTilemaker;
using OpenGraphTilemaker.OpenGraph;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace OpenGraphTilemakerWeb.App.Features.Tiles
{
    public partial class TilesState
    {
        public string SortProperty { get; private set; }
        public SortOrder SortOrder { get; private set; }
        public List<OpenGraphMetadata> Tiles { get; private set; }
    }
}