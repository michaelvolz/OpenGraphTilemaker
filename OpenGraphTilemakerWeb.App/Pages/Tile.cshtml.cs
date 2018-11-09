using System.Threading.Tasks;
using Microsoft.AspNetCore.Blazor.Components;
using OpenGraphTilemaker;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace OpenGraphTilemakerWeb.App.Pages
{
    public class TileModel : BlazorComponent
    {
        [Inject] private AppState AppState { get; set; }

        [Parameter] protected OpenGraphMetadata OpenGraphMetadata { get; set; }

        protected override void OnInit()
        {
            AppState.OnSort += StateHasChanged;
        }
    }
}