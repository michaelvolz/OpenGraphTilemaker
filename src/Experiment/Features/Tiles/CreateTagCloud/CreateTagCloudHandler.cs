using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using Common;
using Common.TagCloud;
using Experiment.Features.Tiles.CreateTagCloud;

// ReSharper disable MemberCanBePrivate.Global

namespace Experiment.Features.Tiles
{
    public partial class TilesState
    {
        [IoC]
        public class CreateTagCloudHandler : RequestHandler<CreateTagCloudRequest, TilesState>
        {
            public CreateTagCloudHandler(IStore store) : base(store) { }

            public TilesState TilesState => Store.GetState<TilesState>();

            public override async Task<TilesState> Handle(CreateTagCloudRequest req, CancellationToken token) {
                var tagCloud = new TagCloud();

                foreach (var tile in req.OriginalTiles)
                    await tagCloud.InsertAsync(tile.Title, tile.Description, tile.SiteName);

                //TilesState.TagCloud = from entry in tagCloud.Cloud orderby entry.Key select entry;

                return TilesState;
            }
        }
    }
}
