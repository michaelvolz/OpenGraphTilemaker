using System.Collections.Generic;
using MediatR;
using OpenGraphTilemaker.OpenGraph;
using OpenGraphTilemaker.Web.Client.Features.Tiles;

namespace OpenGraphTilemaker.Web.Client.Features.Counter
{
    public class CreateTagCloudRequest : IRequest<TilesState>
    {
        public List<OpenGraphMetadata> OriginalTiles { get; set; }
    }
}
