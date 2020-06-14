using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using BlazorState;
using Common;
using Common.Logging;
using Domain.OpenGraphTilemaker.OpenGraph;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Experiment.Features.OpenGraphTiles
{
    public partial class TilesState
    {
        [IoC]
        public class SortTilesHandler : ActionHandler<SortTilesRequest>
        {
            private readonly ILogger<SortTilesHandler> _logger = ApplicationLogging.CreateLogger<SortTilesHandler>();

            public SortTilesHandler(IStore store)
                : base(store) { }

            private TilesState TilesState => Store.GetState<TilesState>();

            public override Task<Unit> Handle(SortTilesRequest aAction, CancellationToken aCancellationToken)
            {
                Guard.Against.Null(aAction, nameof(aAction));
                Guard.Against.Null(aAction.CurrentTiles, nameof(aAction.CurrentTiles));

                if (!string.IsNullOrWhiteSpace(aAction.SortProperty))
                    TilesState.SortProperty = aAction.SortProperty;

                if (aAction.SortOrder != SortOrder.Undefined)
                    TilesState.SortOrder = aAction.SortOrder;

                SortTiles(aAction.CurrentTiles!);

                _logger.LogInformation("SortTilesHandler for: '{SortProperty}, {SortOrder}'", TilesState.SortProperty, TilesState.SortOrder);

                return Unit.Task;
            }

            private void SortTiles(List<OpenGraphMetadata> tiles)
            {
                switch (TilesState.SortProperty)
                {
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
