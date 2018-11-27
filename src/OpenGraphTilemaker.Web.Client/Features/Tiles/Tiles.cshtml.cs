using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Blazor.RenderTree;
using OpenGraphTilemaker.OpenGraph;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public class TilesModel : BlazorComponentStateful
    {
        protected TilesState TilesState => Store.GetState<TilesState>();

        /// <summary>
        ///     OnInit
        /// </summary>
        protected override async Task OnInitAsync() {
            if (!TilesState.OriginalTiles.Any()) {
                await RequestAsync(new InitializeTilesRequest());
                StateHasChanged();
            }
        }

        /// <summary>
        ///     BuildRenderTree
        /// </summary>
        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            SearchIfUpdatedAsync().GetAwaiter().GetResult();

            base.BuildRenderTree(builder);
        }

        protected async Task OnSortPropertyButtonClick() {
            var sortProperty = TilesState.SortProperty != nameof(OpenGraphMetadata.Title)
                ? nameof(OpenGraphMetadata.Title)
                : nameof(OpenGraphMetadata.BookmarkTime);
            await RequestAsync(new SortTilesRequest { SortProperty = sortProperty });
        }

        protected async Task OnSortOrderButtonClick() {
            var sortOrder = TilesState.SortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            await RequestAsync(new SortTilesRequest { SortOrder = sortOrder });
        }

        protected async Task OnSearchButtonClick() {
            await SearchIfUpdatedAsync();
        }

        private async Task SearchIfUpdatedAsync() {
            if (TilesState.LastSearchText != TilesState.SearchText) {
                await RequestAsync(new SearchTilesRequest { SearchText = TilesState.SearchText });
                await RequestAsync(new SortTilesRequest());
            }
        }
    }
}
