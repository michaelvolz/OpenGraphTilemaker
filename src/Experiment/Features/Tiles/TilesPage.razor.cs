using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.Tiles
{
    public class TilesPageModel : BlazorComponentStateful<TilesPageModel>
    {
        [UsedImplicitly] private const int OneSecondInMilliseconds = 1000;

        protected List<OpenGraphMetadata> OriginalTiles { get; private set; } = new List<OpenGraphMetadata>();

        protected bool Loading() => !OriginalTiles.Any() && IsLoading;

        protected override async Task OnInitAsync()
        {
            Logger.LogInformation("### {MethodName} loading data...", nameof(OnInitAsync));

            // for testing purposes only!
            //await Task.Delay(1 * OneSecondInMilliseconds);

            var response = await RequestAsync(new FetchTilesRequest());
            OriginalTiles = response.OriginalTiles;
            IsLoading = false;

            Logger.LogInformation("### {MethodName} loading data finished! {Count}", nameof(OnInitAsync), OriginalTiles?.Count);

            StateHasChanged();
        }
    }
}