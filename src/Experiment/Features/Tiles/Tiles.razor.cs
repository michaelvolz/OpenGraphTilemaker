using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Common;
using Experiment.Features.App;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using OpenGraphTilemaker.OpenGraph;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace Experiment.Features.Tiles
{
    public class TilesModel : BlazorComponentStateful<TilesModel>
    {
        [Parameter] public string Class { get; set; } = string.Empty;
        [Parameter] public List<OpenGraphMetadata>? OriginalTiles { get; set; }

        protected TilesState State => Store.GetState<TilesState>();

        protected override async Task OnParametersSetAsync()
        {
            Guard.Against.Null(() => OriginalTiles);

            if (OriginalTiles.Any() && !State.CurrentTiles.Any())
            {
                Logger.LogInformation("### {MethodName} Count: {Count}", nameof(OnParametersSetAsync), OriginalTiles!.Count);

                await SearchAsync(State.SearchText);
                await RequestAsync(new TilesState.CreateTagCloudRequest {OriginalTiles = OriginalTiles});

                IsLoading = false;
            }
        }

        protected bool Loaded() => OriginalTiles.Any();
        protected bool Empty() => !State.CurrentTiles.Any() && !IsLoading;
        protected bool Any() => State.CurrentTiles.Any();

        protected bool TagCloudExists() => State?.TagCloud != null && State.TagCloud.Any();

        protected async Task OnSortProperty(string sortProperty) => await SortByPropertyAsync(sortProperty);
        protected async Task OnSortOrder(SortOrder sortOrder) => await SortByOrderAsync(sortOrder);
        protected async Task OnSearch(string searchText) => await SearchAsync(searchText);

        private async Task SortByOrderAsync(SortOrder sortOrder)
        {
            sortOrder = sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            await RequestAsync(new TilesState.SortTilesRequest {CurrentTiles = State.CurrentTiles, SortOrder = sortOrder});

            StateHasChanged();
        }

        private async Task SortByPropertyAsync(string sortProperty)
        {
            sortProperty = sortProperty != nameof(OpenGraphMetadata.Title)
                ? nameof(OpenGraphMetadata.Title)
                : nameof(OpenGraphMetadata.BookmarkTime);
            await RequestAsync(new TilesState.SortTilesRequest {CurrentTiles = State.CurrentTiles, SortProperty = sortProperty});

            StateHasChanged();
        }

        private async Task SearchAsync(string searchText)
        {
            if (OriginalTiles == null || !OriginalTiles.Any()) return;

            await RequestAsync(new TilesState.SearchTilesRequest {OriginalTiles = OriginalTiles, SearchText = searchText});
            await RequestAsync(new TilesState.SortTilesRequest {CurrentTiles = State.CurrentTiles});

            StateHasChanged();
        }
    }
}