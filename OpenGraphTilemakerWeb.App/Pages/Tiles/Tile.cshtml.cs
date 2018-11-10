using Microsoft.AspNetCore.Blazor.Components;
using OpenGraphTilemaker;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace OpenGraphTilemakerWeb.App.Pages.Tiles
{
    public class TileModel : BlazorComponent
    {
        [Parameter] protected OpenGraphMetadata OpenGraphMetadata { get; set; }
    }
}