using Microsoft.AspNetCore.Blazor.Components;
using OpenGraphTilemaker;

namespace OpenGraphTilemakerWeb.App.Pages
{
    public class TileXModel : BlazorComponent
    {
        [Parameter] protected OpenGraphMetadata OpenGraphMetadata { get; set; }
    }
}