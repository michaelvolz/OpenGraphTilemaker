using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using BlazorState;
using Common;
using Common.Extensions;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using OpenGraphTilemaker.OpenGraph;

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public partial class TilesState
    {
        [IoC]
        public class SearchTilesHandler : RequestHandler<SearchTilesRequest, TilesState>
        {
            [NotNull] private readonly ILogger<SearchTilesHandler> _logger;

            public SearchTilesHandler([NotNull] ILogger<SearchTilesHandler> logger, IStore store) : base(store) {
                _logger = Guard.Against.Null(() => logger);
            }

            private TilesState TilesState => Store.GetState<TilesState>();

            public override Task<TilesState> Handle(SearchTilesRequest req, CancellationToken token) {
                if (TilesState.LastSearchText != TilesState.SearchText) {
                    
                    if (TilesState.SearchText.IsNullOrWhiteSpace()) {
                        TilesState.CurrentTiles = TilesState.OriginalTiles;
                    }
                    else {
                        var search = TilesState.SearchText.ToLowerInvariant();

                        TilesState.CurrentTiles = TilesState.OriginalTiles
                            .Where(
                                t => t.Title != null && t.Title.ToLowerInvariant().Contains(search)
                                     || t.Description != null && t.Description.ToLowerInvariant().Contains(search)
                                     || t.SiteName != null && t.SiteName.ToLowerInvariant().Contains(search)
                            ).ToList();

                        _logger.LogInformation($"SearchTilesHandler for: '{TilesState.SearchText}', Count: {TilesState.CurrentTiles.Count}");
                    }

                    TilesState.LastSearchText = TilesState.SearchText;
                }

                return Task.FromResult(TilesState);
            }
        }
    }
}
