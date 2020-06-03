using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using Common;
using Common.Extensions;
using Common.Logging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Experiment.Features.Tiles
{
    public partial class TilesState
    {
        [IoC]
        public class SearchTilesHandler : ActionHandler<SearchTilesRequest>
        {
            private readonly ILogger<SearchTilesHandler> _logger = ApplicationLogging.CreateLogger<SearchTilesHandler>();

            public SearchTilesHandler(IStore store) : base(store) { }

            private TilesState TilesState => Store.GetState<TilesState>();

            public override Task<Unit> Handle(SearchTilesRequest req, CancellationToken token) {
                TilesState.SearchText = req.SearchText;

                if (TilesState.SearchText.IsNullOrWhiteSpace()) {
                    TilesState.CurrentTiles = req.OriginalTiles;
                }
                else {
                    var search = TilesState.SearchText.ToUpperInvariant();

                    TilesState.CurrentTiles = req.OriginalTiles
                        .Where(
                            t => t.Title.ToUpperInvariant().Contains(search, StringComparison.InvariantCulture)
                                 || t.Description.ToUpperInvariant().Contains(search, StringComparison.InvariantCulture)
                                 || t.SiteName.ToUpperInvariant().Contains(search, StringComparison.InvariantCulture)
                        ).ToList();
                }

                TilesState.LastSearchText = TilesState.SearchText;

                _logger.LogInformation("SearchTilesHandler for: {SearchText}, Count: {Count}", TilesState.SearchText, TilesState.CurrentTiles.Count);

                return Unit.Task;
            }
        }
    }
}
