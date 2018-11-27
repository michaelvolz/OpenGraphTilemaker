using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using BlazorState;
using Common;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using OpenGraphTilemaker.OpenGraph;

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public partial class TilesState
    {
        [IoC]
        public class SortTilesHandler : RequestHandler<SortTilesRequest, TilesState>
        {
            [NotNull] private readonly ILogger<SortTilesHandler> _logger;

            public SortTilesHandler([NotNull] ILogger<SortTilesHandler> logger, IStore store) : base(store) {
                _logger = Guard.Against.Null(() => logger);
            }

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
