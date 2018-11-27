using Common;
using MediatR;

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public class SearchTilesRequest : IRequest<TilesState>
    {
        public string SearchText { get; set; }
    }
}
