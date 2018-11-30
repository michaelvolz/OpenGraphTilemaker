using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using Common;
using Common.Logging;
using Microsoft.Extensions.Logging;
using OpenGraphTilemaker.OpenGraph;

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public partial class TilesState
    {
        [IoC]
        public class SortTilesHandler : RequestHandler<SortTilesRequest, TilesState>
        {
            private readonly ILogger<SortTilesHandler> _logger = ApplicationLogging.CreateLogger<SortTilesHandler>();

            public SortTilesHandler(IStore store) : base(store) { }

            private TilesState TilesState => Store.GetState<TilesState>();

            public override Task<TilesState> Handle(SortTilesRequest req, CancellationToken token) {
                if (req.SortProperty != null)
                    TilesState.SortProperty = req.SortProperty;

                if (req.SortOrder != SortOrder.Undefined)
                    TilesState.SortOrder = req.SortOrder;

                SortTiles();

                _logger.LogInformation($"SortTilesHandler for: '{TilesState.SortProperty}, {TilesState.SortOrder}'");

                return Task.FromResult(TilesState);
            }

            private void SortTiles() {
                switch (TilesState.SortProperty) {
                    case nameof(OpenGraphMetadata.Title):
                        TilesState.CurrentTiles = TilesState.SortOrder == SortOrder.Ascending
                            ? TilesState.CurrentTiles.OrderBy(f => f.Title).ToList()
                            : TilesState.CurrentTiles.OrderByDescending(f => f.Title).ToList();
                        break;

                    case nameof(OpenGraphMetadata.BookmarkTime):
                        TilesState.CurrentTiles = TilesState.SortOrder == SortOrder.Ascending
                            ? TilesState.CurrentTiles.OrderBy(f => f.BookmarkTime).ToList()
                            : TilesState.CurrentTiles.OrderByDescending(f => f.BookmarkTime).ToList();
                        break;
                }
            }
        }
    }
}
