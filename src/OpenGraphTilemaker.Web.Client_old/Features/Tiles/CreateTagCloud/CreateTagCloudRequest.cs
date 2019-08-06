using System.Collections.Generic;
using MediatR;
using OpenGraphTilemaker.OpenGraph;

namespace OpenGraphTilemaker.Web.Client.Features.Tiles.CreateTagCloud
{
    public class CreateTagCloudRequest : IRequest<TilesState>
    {
        public List<OpenGraphMetadata> OriginalTiles { get; set; }
    }
}
