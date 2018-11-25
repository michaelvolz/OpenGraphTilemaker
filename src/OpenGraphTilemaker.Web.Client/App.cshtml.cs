using System.Threading.Tasks;
using BlazorState.Behaviors.ReduxDevTools;
using Microsoft.AspNetCore.Blazor.Components;

namespace OpenGraphTilemaker.Web.Client
{
#pragma warning disable SA1649 // File name should match first type name
    public class AppModel : BlazorComponent, IComponent
#pragma warning restore SA1649 // File name should match first type name
    {
        // [Inject] private JsonRequestHandler JsonRequestHandler { get; set; }
        // [Inject] private RouteManager RouteManager { get; set; }
        // [Inject] private ReduxDevToolsInterop ReduxDevToolsInterop { get; set; }

        // protected override async Task OnInitAsync() => await ReduxDevToolsInterop.InitAsync();
    }
}
