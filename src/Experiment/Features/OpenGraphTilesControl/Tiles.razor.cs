using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Common;
using Domain.OpenGraphTilemaker.OpenGraph;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Experiment.Features.OpenGraphTilesControl
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Blazor rule")]
    public partial class Tiles
    {
        [Parameter] public string Class { get; [UsedImplicitly] set; } = string.Empty;
        [Parameter] public IList<OpenGraphMetadata> OriginalTiles { get; private set; } = null!;

        private TilesState State => Store.GetState<TilesState>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            Guard.Against.Null(OriginalTiles, nameof(OriginalTiles));

            if (OriginalTiles.Any() && !State.FilteredAndSortedTiles.Any())
            {
                Logger.LogInformation("### {MethodName} Count: {Count}", nameof(OnParametersSetAsync), OriginalTiles.Count);

                await SearchAsync(State.SearchText);
                await RequestAsync(new TilesState.CreateTagCloudRequest { OriginalTiles = OriginalTiles });

                IsLoading = false;
            }
        }

        protected bool Loaded() => OriginalTiles.Any();
        protected bool Empty() => !State.FilteredAndSortedTiles.Any() && !IsLoading;
        protected bool Any() => State.FilteredAndSortedTiles.Any();

        protected bool TagCloudExists() => State.TagCloud != null && State.TagCloud.Any();

        protected async Task OnSortProperty(string sortProperty) => await SortByPropertyAsync(sortProperty);
        protected async Task OnSortOrder(SortOrder sortOrder) => await SortByOrderAsync(sortOrder);
        protected async Task OnSearch(string searchText) => await SearchAsync(searchText);

        private async Task SortByOrderAsync(SortOrder sortOrder)
        {
            sortOrder = sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            await RequestAsync(new TilesState.SortTilesRequest { CurrentTiles = State.FilteredAndSortedTiles, SortOrder = sortOrder });

            StateHasChanged();
        }

        private async Task SortByPropertyAsync(string sortProperty)
        {
            sortProperty = sortProperty != nameof(OpenGraphMetadata.Title)
                ? nameof(OpenGraphMetadata.Title)
                : nameof(OpenGraphMetadata.BookmarkTime);
            await RequestAsync(new TilesState.SortTilesRequest { CurrentTiles = State.FilteredAndSortedTiles, SortProperty = sortProperty });

            StateHasChanged();
        }

        private async Task SearchAsync(string searchText)
        {
            // ReSharper disable once ConstantConditionalAccessQualifier
            if (OriginalTiles?.Any() != true) return;

            await RequestAsync(new TilesState.SearchTilesRequest { OriginalTiles = OriginalTiles, SearchText = searchText });
            await RequestAsync(new TilesState.SortTilesRequest { CurrentTiles = State.FilteredAndSortedTiles });

            StateHasChanged();
        }
    }
}
