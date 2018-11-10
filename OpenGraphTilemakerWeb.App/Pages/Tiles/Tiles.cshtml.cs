using System.Linq;
using OpenGraphTilemaker;
using OpenGraphTilemakerWeb.App.ClientApp;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace OpenGraphTilemakerWeb.App.Pages.Tiles
{
    public class TilesModel : BlazorComponentStateful
    {
        protected TilesState TilesState => Store.GetState<TilesState>();

        protected override void OnInit()
        {
            if (!TilesState.Tiles.Any()) Request(new InitializeTilesRequest());
        }

        protected void OnSortPropertyButtonClick()
        {
            var sortProperty = TilesState.SortProperty != nameof(OpenGraphMetadata.Title)
                ? nameof(OpenGraphMetadata.Title)
                : nameof(OpenGraphMetadata.SourcePublishTime);
            Request(new SortTilesRequest {SortProperty = sortProperty});
        }

        protected void OnSortOrderButtonClick()
        {
            var sortOrder = TilesState.SortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            Request(new SortTilesRequest {SortOrder = sortOrder});
        }
    }
}