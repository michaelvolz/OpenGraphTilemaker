using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using BlazorState;
using Common;
using Common.Logging;
using MediatR;
using Microsoft.Extensions.Logging;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.Tiles
{
    public partial class TilesState
    {
        [IoC]
        public class SortTilesHandler : ActionHandler<SortTilesRequest>
        {
            private readonly ILogger<SortTilesHandler> _logger = ApplicationLogging.CreateLogger<SortTilesHandler>();

            public SortTilesHandler(IStore store) : base(store) { }

            private TilesState TilesState => Store.GetState<TilesState>();

            public override Task<Unit> Handle(SortTilesRequest req, CancellationToken token)
            {
                Guard.Against.Null(() => req.CurrentTiles);

                if (!string.IsNullOrWhiteSpace(req.SortProperty))
                    TilesState.SortProperty = req.SortProperty;

                if (req.SortOrder != SortOrder.Undefined)
                    TilesState.SortOrder = req.SortOrder;

                SortTiles(req.CurrentTiles!);

                _logger.LogInformation("SortTilesHandler for: '{SortProperty}, {SortOrder}'", TilesState.SortProperty, TilesState.SortOrder);

                return Unit.Task;
            }

            private void SortTiles(List<OpenGraphMetadata> tiles) {
                switch (TilesState.SortProperty) {
                    case nameof(OpenGraphMetadata.Title):
                        TilesState.FilteredAndSortedTiles = TilesState.SortOrder == SortOrder.Ascending
                            ? tiles.OrderBy(f => f.Title).ToList()
                            : tiles.OrderByDescending(f => f.Title).ToList();
                        break;

                    case nameof(OpenGraphMetadata.BookmarkTime):
                        TilesState.FilteredAndSortedTiles = TilesState.SortOrder == SortOrder.Ascending
                            ? tiles.OrderBy(f => f.BookmarkTime).ToList()
                            : tiles.OrderByDescending(f => f.BookmarkTime).ToList();
                        break;
                }
            }
        }
    }
}
