using Microsoft.AspNetCore.Components;
using OpenGraphTilemaker.OpenGraph;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Experiment.Features.Tiles
{
    public class TileModel : ComponentBase
    {
#nullable disable
        [Parameter] protected OpenGraphMetadata OpenGraphMetadata { get; set; }
#nullable enable
    }
}
