using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenGraphTilemaker.OpenGraph;

namespace Experiment.Features.Tiles
{
    public class TilesPageModel : BlazorComponentStateful<TilesPageModel>
    {
        private const int OneSecondInMilliseconds = 1000;

        protected List<OpenGraphMetadata> OriginalTiles { get; set; } = new List<OpenGraphMetadata>();

        protected bool Loading() => !OriginalTiles.Any() && IsLoading;

        protected override async Task OnInitAsync()
        {
            Logger.LogInformation($"### {nameof(OnInitAsync)} loading data...");

            // for testing purposes only!
            //await Task.Delay(1 * OneSecondInMilliseconds);

            var response = await RequestAsync(new FetchTilesRequest());
            OriginalTiles = response.OriginalTiles;
            IsLoading = false;

            Logger.LogInformation($"### {nameof(OnInitAsync)} loading data finished! " + OriginalTiles?.Count);

            StateHasChanged();
        }
    }
}
