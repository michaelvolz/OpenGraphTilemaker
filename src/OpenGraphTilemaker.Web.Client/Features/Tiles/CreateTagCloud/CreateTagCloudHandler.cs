using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using Common;
using Common.TagCloud;
using OpenGraphTilemaker.Web.Client.Features.Tiles.CreateTagCloud;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
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
                foreach (var tile in req.OriginalTiles) {
                    await tagCloud.InsertAsync(tile.Title);
                    await tagCloud.InsertAsync(tile.Description);
                }

                TilesState.TagCloud = from entry in tagCloud.Cloud orderby entry.Key select entry;

                return TilesState;
            }
        }
    }
}
