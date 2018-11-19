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
            private readonly Time _time;

            public SortTilesHandler([NotNull] ILogger<SortTilesHandler> logger, [NotNull] Time time, IStore store) : base(store) {
                _logger = Guard.Against.Null(() => logger);
                _time = Guard.Against.Null(() => time);
            }

            private TilesState TilesState => Store.GetState<TilesState>();

            public override Task<TilesState> Handle(SortTilesRequest req, CancellationToken token) {
                if (req.SortProperty != null)
                    TilesState.SortProperty = req.SortProperty;

                if (req.SortOrder != SortOrder.Undefined)
                    TilesState.SortOrder = req.SortOrder;

                _time.This(SortTiles, nameof(SortTilesHandler));

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
