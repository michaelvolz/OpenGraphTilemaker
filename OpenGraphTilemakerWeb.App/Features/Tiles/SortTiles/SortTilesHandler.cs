using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using Common;
using OpenGraphTilemaker;
using OpenGraphTilemaker.OpenGraph;

namespace OpenGraphTilemakerWeb.App.Features.Tiles
{
    public partial class TilesState
    {
        [IoC]
        public class SortTilesHandler : RequestHandler<SortTilesRequest, TilesState>
        {
            public SortTilesHandler(IStore store) : base(store) { }

            private TilesState TilesState => Store.GetState<TilesState>();

            public override Task<TilesState> Handle(SortTilesRequest req, CancellationToken token) {
                if (req.SortProperty != null)
                    TilesState.SortProperty = req.SortProperty;

                if (req.SortOrder != SortOrder.Undefined)
                    TilesState.SortOrder = req.SortOrder;

                SortTiles();

                return Task.FromResult(TilesState);
            }

            private void SortTiles() {
                switch (TilesState.SortProperty) {
                    case nameof(OpenGraphMetadata.Title):
                        TilesState.Tiles = TilesState.SortOrder == SortOrder.Ascending
                            ? TilesState.Tiles.OrderBy(f => f.Title).ToList()
                            : TilesState.Tiles.OrderByDescending(f => f.Title).ToList();
                        break;

                    case nameof(OpenGraphMetadata.SourcePublishTime):
                        TilesState.Tiles = TilesState.SortOrder == SortOrder.Ascending
                            ? TilesState.Tiles.OrderBy(f => f.SourcePublishTime).ToList()
                            : TilesState.Tiles.OrderByDescending(f => f.SourcePublishTime).ToList();
                        break;
                }
            }
        }
    }
}