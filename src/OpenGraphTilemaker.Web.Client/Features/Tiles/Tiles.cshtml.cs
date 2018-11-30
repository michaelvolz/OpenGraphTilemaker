using System;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
using Microsoft.Extensions.Logging;
using OpenGraphTilemaker.OpenGraph;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public class TilesModel : BlazorComponentStateful<TilesModel>
    {
        private const int OneSecondInMilliseconds = 1000;
        private readonly string _logPrefix;

        protected SortingAndSearch SortComponent { get; set; }
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="TileModel" /> class.
        /// </summary>
        protected TilesModel() => _logPrefix = $"### {nameof(TileModel)}";

        protected TilesState State => Store.GetState<TilesState>();

        protected bool Loading() => !State.CurrentTiles.Any() && IsLoading;
        protected bool Empty() => State.CurrentTiles.Any() || IsLoading;
        protected bool Any() => State.CurrentTiles.Any();

        /// <summary>
        ///     OnInit.
        /// </summary>
        protected override async Task OnInitAsync() {
            Logger.LogInformation($"{_logPrefix}.{nameof(OnInitAsync)}");

            if (!State.OriginalTiles.Any()) {
                Logger.LogInformation($"{_logPrefix}.{nameof(OnInitAsync)} loading data...");

                IsLoading = true;
                // for testing purposes only!
                await Task.Delay(0 * OneSecondInMilliseconds);
                await RequestAsync(new InitializeTilesRequest());

                StateHasChanged();
            }

            IsLoading = false;
        }

        protected async void OnSortProperty(string sortProperty) {
            SortComponent.TextInjectedFromParent("myParentText");

            sortProperty = sortProperty != nameof(OpenGraphMetadata.Title)
                ? nameof(OpenGraphMetadata.Title)
                : nameof(OpenGraphMetadata.BookmarkTime);
            await RequestAsync(new SortTilesRequest { SortProperty = sortProperty });

            StateHasChanged();
        }

        protected async void OnSortOrder(SortOrder sortOrder) {
            sortOrder = sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            await RequestAsync(new SortTilesRequest { SortOrder = sortOrder });

            StateHasChanged();
        }

        protected async void OnSearch(string searchText) {
            await Task.FromResult(0);

            SearchIfUpdatedAsync(searchText).GetAwaiter().GetResult();

            //Logger.Debug("State: {@state}", State.CurrentTiles);

            StateHasChanged();
        }

        private async Task SearchIfUpdatedAsync(string searchText) {
            await RequestAsync(new SearchTilesRequest { SearchText = searchText });
            await RequestAsync(new SortTilesRequest());
        }
    }
}
