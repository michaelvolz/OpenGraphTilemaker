using Microsoft.AspNetCore.Blazor.Components;
using OpenGraphTilemaker;

namespace OpenGraphTilemakerWeb.App.Pages.Tiles
{
    public class TileModel : BlazorComponent
    {
        [Parameter] protected OpenGraphMetadata OpenGraphMetadata { get; set; }
    }
}