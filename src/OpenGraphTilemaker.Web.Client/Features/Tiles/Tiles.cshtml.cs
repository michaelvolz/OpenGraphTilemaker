using System.Linq;
using System.Threading.Tasks;
using Common;
using OpenGraphTilemaker.OpenGraph;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public class TilesModel : BlazorComponentStateful
    {
        protected TilesState TilesState => Store.GetState<TilesState>();

        protected override async Task OnInitAsync() {
            if (!TilesState.Tiles.Any()) await RequestAsync(new InitializeTilesRequest());

            StateHasChanged();
        }

        protected async Task OnSortPropertyButtonClick() {
            var sortProperty = TilesState.SortProperty != nameof(OpenGraphMetadata.Title)
                ? nameof(OpenGraphMetadata.Title)
                : nameof(OpenGraphMetadata.SourcePublishTime);
            await RequestAsync(new SortTilesRequest { SortProperty = sortProperty });
        }

        protected async Task OnSortOrderButtonClick() {
            var sortOrder = TilesState.SortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            await RequestAsync(new SortTilesRequest { SortOrder = sortOrder });
        }
    }
}
