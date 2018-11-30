using System.Threading.Tasks;
using Microsoft.JSInterop;
using Mono.WebAssembly.Interop;

namespace OpenGraphTilemaker.Web.Client.Features.NavMenu
{
    public class NavMenuModel : BlazorComponentStateful<NavMenuModel>
    {
        protected string NavMenuCSS { get; private set; }

        protected void ToggleNavMenu() => NavMenuCSS = NavMenuCSS == null ? "collapsed" : null;

        protected string ServerModeCSS() => IsClientMode() ? string.Empty : "active";
        protected string ClientModeCSS() => IsClientMode() ? "active" : string.Empty;

        protected async Task ActivateClientMode() => await JSInteropHelpers.NavigateToAsync($"{UriHelper.GetBaseUri()}?mode=client");
        protected async Task ActivateServerMode() => await JSInteropHelpers.NavigateToAsync($"{UriHelper.GetBaseUri()}?mode=server");

        private bool IsClientMode() => JSRuntime.Current is MonoWebAssemblyJSRuntime;
    }
}
