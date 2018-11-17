using System.Threading.Tasks;
using BlazorState.Behaviors.ReduxDevTools;
using BlazorState.Features.JavaScriptInterop;
using BlazorState.Features.Routing;
using Microsoft.AspNetCore.Blazor.Components;

namespace OpenGraphTilemaker.Web.Client
{
    public class AppModel : BlazorComponent, IComponent
    {
//        [Inject] private JsonRequestHandler JsonRequestHandler { get; set; }

//        [Inject] private RouteManager RouteManager { get; set; }

        [Inject] private ReduxDevToolsInterop ReduxDevToolsInterop { get; set; }

        protected override async Task OnInitAsync() => await ReduxDevToolsInterop.InitAsync();
    }
}