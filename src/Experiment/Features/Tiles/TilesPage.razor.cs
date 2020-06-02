using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Experiment.Features.App;
using Microsoft.Extensions.Logging;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.Tiles
{
    public class TilesPageModel : BlazorComponentStateful<TilesPageModel>
    {
        protected List<OpenGraphMetadata> OriginalTiles { get; private set; } = new List<OpenGraphMetadata>();

        protected bool Loading() => !OriginalTiles.Any() && IsLoading;

        protected override async Task OnInitializedAsync()
        {
            Logger.LogInformation("### {MethodName} loading data...", nameof(OnInitializedAsync));

            await Time.ThisAsync(() => RequestAsync(new TilesState.FetchTilesRequest()), nameof(TilesState.FetchTilesRequest), Logger);

            OriginalTiles = Store.GetState<TilesState>().OriginalTiles;
            IsLoading = false;

            Logger.LogInformation("### {MethodName} loading data finished! {Count}", nameof(OnInitializedAsync), OriginalTiles?.Count);

            StateHasChanged();
        }
    }
}