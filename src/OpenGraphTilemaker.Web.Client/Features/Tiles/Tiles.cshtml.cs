using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Blazor.RenderTree;
using Microsoft.Extensions.Logging;
using OpenGraphTilemaker.OpenGraph;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public class TilesModel : BlazorComponentStateful
    {
        private const int OneSecondInMilliseconds = 1000;
        private readonly string _logPrefix;

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
            Log.LogInformation($"{_logPrefix}.{nameof(OnInitAsync)}");

            if (!State.OriginalTiles.Any()) {
                Log.LogInformation($"{_logPrefix}.{nameof(OnInitAsync)} loading data...");

                IsLoading = true;
                // for testing purposes only!
                await Task.Delay(5 * OneSecondInMilliseconds);
                await RequestAsync(new InitializeTilesRequest());

                StateHasChanged();
            }

            IsLoading = false;
        }

        /// <summary>
        ///     BuildRenderTree.
        /// </summary>
        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            // Fires when 'enter' was pressed in the searchBox  or  searchBox -> blur
            SearchIfUpdatedAsync().GetAwaiter().GetResult();

            base.BuildRenderTree(builder);
        }

        protected async Task OnSortProperty() {
            var sortProperty = State.SortProperty != nameof(OpenGraphMetadata.Title)
                ? nameof(OpenGraphMetadata.Title)
                : nameof(OpenGraphMetadata.BookmarkTime);
            await RequestAsync(new SortTilesRequest { SortProperty = sortProperty });
        }

        protected async Task OnSortOrder() {
            var sortOrder = State.SortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            await RequestAsync(new SortTilesRequest { SortOrder = sortOrder });
        }

        protected async Task OnSearch() => await SearchIfUpdatedAsync();

        private async Task SearchIfUpdatedAsync() {
            if (State.LastSearchText != State.SearchText) {
                await RequestAsync(new SearchTilesRequest { SearchText = State.SearchText });
                await RequestAsync(new SortTilesRequest());
            }
        }
    }
}
