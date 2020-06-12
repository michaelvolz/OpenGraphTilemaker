using System.Threading.Tasks;
using BlazorState.Features.JavaScriptInterop;
using BlazorState.Features.Routing;
using BlazorState.Pipeline.ReduxDevTools;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace Experiment
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "TODO")]
    public class AppBase : ComponentBase
    {
        [Inject] private JsonRequestHandler JsonRequestHandler { get; [UsedImplicitly] set; } = null!;
        [Inject] private ReduxDevToolsInterop ReduxDevToolsInterop { get; [UsedImplicitly] set; } = null!;

        // Injected so it is created by the container. Even though the IDE says it is not used, it is.
        [Inject] private RouteManager RouteManager { get; [UsedImplicitly] set; } = null!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await ReduxDevToolsInterop.InitAsync();
            await JsonRequestHandler.InitAsync();
        }
    }
}
