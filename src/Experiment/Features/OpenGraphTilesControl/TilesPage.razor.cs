using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Experiment.Features.OpenGraphTilesControl
{
    public partial class TilesPage
    {
        protected override async Task OnInitializedAsync()
        {
            if (!Store.GetState<TilesState>().OriginalTiles.Any())
                await InitializeDataAsync();

            IsLoading = false;
        }

        private bool Loading() => !Store.GetState<TilesState>().OriginalTiles.Any() && IsLoading;

        private async Task InitializeDataAsync()
        {
            Logger.LogInformation("### {MethodName} loading data...", nameof(InitializeDataAsync));

            await RequestAsync(new TilesState.FetchTilesRequest());

            Logger.LogInformation(
                "### {MethodName} loading data finished! {Count}", nameof(InitializeDataAsync), Store.GetState<TilesState>().OriginalTiles.Count);
        }
    }
}
