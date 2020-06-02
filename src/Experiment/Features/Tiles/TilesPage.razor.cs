using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Experiment.Features.Tiles
{
    public partial class TilesPage
    {
        private bool Loading() => !Store.GetState<TilesState>().OriginalTiles.Any() && IsLoading;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender) return;

            if (!Store.GetState<TilesState>().OriginalTiles.Any())
                await InitializeData();

            IsLoading = false;
            StateHasChanged();
        }

        private async Task InitializeData()
        {
            Logger.LogInformation("### {MethodName} loading data...", nameof(OnInitializedAsync));

            await Time.ThisAsync(() => RequestAsync(new TilesState.FetchTilesRequest()), nameof(TilesState.FetchTilesRequest), Logger);

            Logger.LogInformation("### {MethodName} loading data finished! {Count}", nameof(OnInitializedAsync),
                Store.GetState<TilesState>().OriginalTiles.Count);
        }
    }
}