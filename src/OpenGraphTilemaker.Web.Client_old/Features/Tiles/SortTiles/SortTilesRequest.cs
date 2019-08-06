using System.Collections.Generic;
using Common;
using MediatR;
using OpenGraphTilemaker.OpenGraph;

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public class SortTilesRequest : IRequest<TilesState>
    {
        public List<OpenGraphMetadata> CurrentTiles { get; set; }

        public string SortProperty { get; set; }
        public SortOrder SortOrder { get; set; }
    }
}
