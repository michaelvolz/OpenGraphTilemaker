using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Common;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.Extensions.Logging;
using OpenGraphTilemaker.OpenGraph;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public class TilesModel : BlazorComponentStateful<TilesModel>
    {
        [Parameter] private protected string Class { get; set; }
        [Parameter] private protected List<OpenGraphMetadata> OriginalTiles { get; set; }

        protected SortingAndSearch SortComponent { get; set; }
        protected TilesState State => Store.GetState<TilesState>();

        protected override async Task OnParametersSetAsync() {
            Guard.Against.Null(() => OriginalTiles);

            if (OriginalTiles.Any() && !State.CurrentTiles.Any()) {
                Logger.LogInformation($"### {nameof(OnParametersSetAsync)} Count: " + OriginalTiles.Count);
                await SearchAsync(State.SearchText);

                IsLoading = false;
            }
        }

        protected bool Loaded() => !IsLoading && OriginalTiles.Any();
        protected bool Empty() => !State.CurrentTiles.Any() && !IsLoading;
        protected bool Any() => State.CurrentTiles.Any();

        protected async Task OnSortProperty(string sortProperty) => await SortByProperty(sortProperty);
        protected async Task OnSortOrder(SortOrder sortOrder) => await SortByOrder(sortOrder);
        protected async Task OnSearch(string searchText) => await SearchAsync(searchText);


        private async Task SortByOrder(SortOrder sortOrder) {
            sortOrder = sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            await RequestAsync(new SortTilesRequest { CurrentTiles = State.CurrentTiles, SortOrder = sortOrder });

            StateHasChanged();
        }

        private async Task SortByProperty(string sortProperty) {
            SortComponent.TextInjectedFromParent("myParentText");

            sortProperty = sortProperty != nameof(OpenGraphMetadata.Title)
                ? nameof(OpenGraphMetadata.Title)
                : nameof(OpenGraphMetadata.BookmarkTime);
            await RequestAsync(new SortTilesRequest { CurrentTiles = State.CurrentTiles, SortProperty = sortProperty });

            StateHasChanged();
        }

        private async Task SearchAsync(string searchText) {
            if (OriginalTiles.Any()) {
                await RequestAsync(new SearchTilesRequest { OriginalTiles = OriginalTiles, SearchText = searchText });
                await RequestAsync(new SortTilesRequest { CurrentTiles = State.CurrentTiles });

                StateHasChanged();
            }
        }
    }
}
