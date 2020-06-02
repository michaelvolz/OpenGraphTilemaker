using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.Tiles
{
    public partial class TilesPage
    {
        // TODO: Duplication, remove!
        private List<OpenGraphMetadata> OriginalTiles { get; set; } = new List<OpenGraphMetadata>();

        private bool Loading() => !OriginalTiles.Any() && IsLoading;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender) return;

            if (Store.GetState<TilesState>().OriginalTiles.Any())
                CopyOriginalTiles();
            else
                await InitializeData();

            IsLoading = false;
            StateHasChanged();
        }

        private async Task InitializeData()
        {
            Logger.LogInformation("### {MethodName} loading data...", nameof(OnInitializedAsync));

            await Time.ThisAsync(() => RequestAsync(new TilesState.FetchTilesRequest()), nameof(TilesState.FetchTilesRequest), Logger);

            CopyOriginalTiles();

            Logger.LogInformation("### {MethodName} loading data finished! {Count}", nameof(OnInitializedAsync), OriginalTiles.Count);
        }

        private void CopyOriginalTiles() => OriginalTiles = Store.GetState<TilesState>().OriginalTiles;
    }
}