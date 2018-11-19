using Common;
using MediatR;

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public class SortTilesRequest : IRequest<TilesState>
    {
        public string SortProperty { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}
