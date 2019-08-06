using Microsoft.AspNetCore.Components;
using OpenGraphTilemaker.OpenGraph;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public class TileModel : ComponentBase
    {
        [Parameter] protected OpenGraphMetadata OpenGraphMetadata { get; set; }
    }
}
