using MediatR;
using OpenGraphTilemaker;

namespace OpenGraphTilemakerWeb.App.Pages.Tiles
{
    public class SortTilesRequest : IRequest<TilesState>
    {
        public string SortProperty { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}