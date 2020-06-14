using System.Threading.Tasks;
using BlazorState.Features.JavaScriptInterop;
using BlazorState.Features.Routing;
using BlazorState.Pipeline.ReduxDevTools;
using Common.Logging;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Experiment
{
    public partial class App
    {
        [Inject] private JsonRequestHandler JsonRequestHandler { get; [UsedImplicitly] set; } = null!;
        [Inject] private ReduxDevToolsInterop ReduxDevToolsInterop { get; [UsedImplicitly] set; } = null!;

        // Injected so it is created by the container. Even though the IDE says it is not used, it is.
        [Inject] [UsedImplicitly] private RouteManager RouteManager { get; [UsedImplicitly] set; } = null!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var logger = ApplicationLogging.CreateLogger<App>();
            logger.LogInformation("Initializing App component...");
            
            await ReduxDevToolsInterop.InitAsync();
            await JsonRequestHandler.InitAsync();

            logger.LogInformation("Initializing App component...done");
        }
    }
}
