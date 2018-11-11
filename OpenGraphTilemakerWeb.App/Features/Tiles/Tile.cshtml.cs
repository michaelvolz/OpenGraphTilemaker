using Microsoft.AspNetCore.Blazor.Components;
using OpenGraphTilemaker;
using OpenGraphTilemaker.OpenGraph;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace OpenGraphTilemakerWeb.App.Features.Tiles
{
    public class TileModel : BlazorComponent
    {
        [Parameter] protected OpenGraphMetadata OpenGraphMetadata { get; set; }
    }
}