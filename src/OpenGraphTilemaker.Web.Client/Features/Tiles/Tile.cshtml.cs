using Microsoft.AspNetCore.Blazor.Components;
using OpenGraphTilemaker.OpenGraph;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace OpenGraphTilemaker.Web.Client.Features.Tiles
{
    public class TileModel : BlazorComponent
    {
        [Parameter] protected OpenGraphMetadata OpenGraphMetadata { get; set; }
    }
}
