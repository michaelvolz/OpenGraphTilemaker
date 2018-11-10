using MediatR;
using OpenGraphTilemaker;

// ReSharper disable UnusedMember.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace OpenGraphTilemakerWeb.App.Pages.Tiles
{
    public class SortTilesRequest : IRequest<TilesState>
    {
        public string SortProperty { get; set; }

        public SortOrder SortOrder { get; set; }
    }
}