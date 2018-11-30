using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorState;
using Common;
using Common.Extensions;
using Common.Logging;
using Microsoft.Extensions.Logging;

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public partial class TilesState
    {
        [IoC]
        public class SearchTilesHandler : RequestHandler<SearchTilesRequest, TilesState>
        {
            private readonly ILogger<SearchTilesHandler> _logger = ApplicationLogging.CreateLogger<SearchTilesHandler>();

            public SearchTilesHandler(IStore store) : base(store) { }

            private TilesState TilesState => Store.GetState<TilesState>();

            public override Task<TilesState> Handle(SearchTilesRequest req, CancellationToken token) {
                TilesState.SearchText = req.SearchText;

                if (TilesState.LastSearchText != TilesState.SearchText) {
                    if (TilesState.SearchText.IsNullOrWhiteSpace()) {
                        TilesState.CurrentTiles = TilesState.OriginalTiles;
                    }
                    else {
                        var search = TilesState.SearchText.ToUpperInvariant();

                        TilesState.CurrentTiles = TilesState.OriginalTiles
                            .Where(
                                t => t.Title != null && t.Title.ToUpperInvariant().Contains(search)
                                     || t.Description != null && t.Description.ToUpperInvariant().Contains(search)
                                     || t.SiteName != null && t.SiteName.ToUpperInvariant().Contains(search)
                            ).ToList();

                        _logger.LogInformation("SearchTilesHandler for: {SearchText}, Count: {Count}", TilesState.SearchText, TilesState.CurrentTiles.Count);
                    }

                    TilesState.LastSearchText = TilesState.SearchText;
                }

                return Task.FromResult(TilesState);
            }
        }
    }
}
