using System.Threading.Tasks;
using Microsoft.JSInterop;
using Mono.WebAssembly.Interop;

namespace OpenGraphTilemaker.Web.Client.Features.NavMenu
{
    public class NavMenuModel : BlazorComponentStateful
    {
        protected string NavMenuCSS { get; private set; }

        protected void ToggleNavMenu() => NavMenuCSS = NavMenuCSS == null ? "collapsed" : null;

        protected string ServerModeCSS() => IsClientMode() ? "" : "active";
        protected string ClientModeCSS() => IsClientMode() ? "active" : "";

        private bool IsClientMode() => JSRuntime.Current is MonoWebAssemblyJSRuntime;

        protected async Task ActivateClientMode() => await JSInteropHelpers.NavigateToAsync($"{UriHelper.GetBaseUri()}?mode=client");
        protected async Task ActivateServerMode() => await JSInteropHelpers.NavigateToAsync($"{UriHelper.GetBaseUri()}?mode=server");
    }
}
