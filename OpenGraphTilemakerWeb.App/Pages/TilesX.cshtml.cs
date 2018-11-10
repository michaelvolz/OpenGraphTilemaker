using System.Collections.Generic;
using Microsoft.AspNetCore.Blazor.Components;
using OpenGraphTilemaker;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace OpenGraphTilemakerWeb.App.Pages
{
    public class TilesXModel : BlazorComponent
    {
        [Inject] protected AppState AppState { get; set; }

        [Parameter] protected List<OpenGraphMetadata> Data { get; set; }

        protected void OnSortPropertyButtonClick()
        {
            var sortProperty = AppState.SortProperty != nameof(OpenGraphMetadata.Title)
                ? nameof(OpenGraphMetadata.Title)
                : nameof(OpenGraphMetadata.SourcePublishTime);
            AppState.SortTiles(sortProperty);
        }

        protected void OnSortOrderButtonClick()
        {
            var sortOrder = AppState.SortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            AppState.SortTiles(null, sortOrder);
        }
    }
}