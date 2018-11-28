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
        protected TilesState State => Store.GetState<TilesState>();

        /// <summary>
        ///     OnInit.
        /// </summary>
        protected override async Task OnInitAsync() {
            if (!State.OriginalTiles.Any()) {
                await RequestAsync(new InitializeTilesRequest());
                StateHasChanged();
            }
        }

        /// <summary>
        ///     BuildRenderTree.
        /// </summary>
        protected override void BuildRenderTree(RenderTreeBuilder builder) {
            UpdateIfChangeHappenedWithoutCustomEvent();

            base.BuildRenderTree(builder);
        }

        protected async Task OnSortPropertyButtonClick() {
            var sortProperty = State.SortProperty != nameof(OpenGraphMetadata.Title)
                ? nameof(OpenGraphMetadata.Title)
                : nameof(OpenGraphMetadata.BookmarkTime);
            await RequestAsync(new SortTilesRequest { SortProperty = sortProperty });
        }

        protected async Task OnSortOrderButtonClick() {
            var sortOrder = State.SortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            await RequestAsync(new SortTilesRequest { SortOrder = sortOrder });
        }

        protected async Task OnSearchButtonClick() => await SearchIfUpdatedAsync();

        private void UpdateIfChangeHappenedWithoutCustomEvent() => SearchIfUpdatedAsync().GetAwaiter().GetResult();

        private async Task SearchIfUpdatedAsync() {
            if (State.LastSearchText != State.SearchText) {
                await RequestAsync(new SearchTilesRequest { SearchText = State.SearchText });
                await RequestAsync(new SortTilesRequest());
            }
        }
    }
}
