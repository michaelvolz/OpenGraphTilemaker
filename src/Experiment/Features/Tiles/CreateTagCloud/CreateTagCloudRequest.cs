using System.Collections.Generic;
using MediatR;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.Tiles
{
    public class CreateTagCloudRequest : IRequest<TilesState>
    {
        public List<OpenGraphMetadata> OriginalTiles { get; set; } = new List<OpenGraphMetadata>();
    }
}
