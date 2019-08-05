#nullable enable

using System.Threading.Tasks;

namespace Experiment.Features.NavMenu
{
    public class NavMenuModel : BlazorComponentStateful<NavMenuModel>
    {
        protected string? NavMenuCSS { get; private set; }

        protected void ToggleNavMenu() => NavMenuCSS = NavMenuCSS == null ? "collapsed" : null;

        protected string ServerModeCSS() => IsClientMode() ? string.Empty : "active";
        protected string ClientModeCSS() => IsClientMode() ? "active" : string.Empty;

        protected async Task ActivateClientMode() =>
            await JSInteropHelpers.NavigateToAsync(ComponentContext, JSRuntime,
                $"{UriHelper.GetBaseUri()}?mode=client");

        protected async Task ActivateServerMode() =>
            await JSInteropHelpers.NavigateToAsync(ComponentContext, JSRuntime,
                $"{UriHelper.GetBaseUri()}?mode=server");

        private bool IsClientMode() => false; //JSRuntime.Current is MonoWebAssemblyJSRuntime;
    }
}