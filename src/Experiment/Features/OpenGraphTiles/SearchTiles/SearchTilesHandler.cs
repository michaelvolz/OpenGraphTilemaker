using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using BlazorState;
using Common;
using Common.Extensions;
using Common.Logging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Experiment.Features.OpenGraphTiles
{
    public partial class TilesState
    {
        [IoC]
        public class SearchTilesHandler : ActionHandler<SearchTilesRequest>
        {
            private readonly ILogger<SearchTilesHandler> _logger = ApplicationLogging.CreateLogger<SearchTilesHandler>();

            public SearchTilesHandler(IStore store)
                : base(store) { }

            private TilesState TilesState => Store.GetState<TilesState>();

            public override Task<Unit> Handle(SearchTilesRequest aAction, CancellationToken aCancellationToken)
            {
                Guard.Against.Null(aAction, nameof(aAction));

                TilesState.SearchText = aAction.SearchText;

                if (TilesState.SearchText.IsNullOrWhiteSpace())
                {
                    TilesState.FilteredAndSortedTiles = aAction.OriginalTiles;
                }
                else
                {
                    var search = TilesState.SearchText.ToUpperInvariant();

                    TilesState.FilteredAndSortedTiles = aAction.OriginalTiles
                        .Where(
                            t => t.Title.ToUpperInvariant().Contains(search, StringComparison.InvariantCulture)
                                 || t.Description.ToUpperInvariant().Contains(search, StringComparison.InvariantCulture)
                                 || t.SiteName.ToUpperInvariant().Contains(search, StringComparison.InvariantCulture))
                        .ToList();
                }

                _logger.LogInformation("SearchTilesHandler for: {SearchText}, Count: {Count}", TilesState.SearchText, TilesState.FilteredAndSortedTiles.Count);

                return Unit.Task;
            }
        }
    }
}
