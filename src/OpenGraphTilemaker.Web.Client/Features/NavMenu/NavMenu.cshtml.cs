using System.Globalization;
using System.Threading.Tasks;

namespace OpenGraphTilemaker.Web.Client.Features.NavMenu
{
    public class NavMenuModel : BlazorComponentStateful
    {
        private NavMenuState State => Store.GetState<NavMenuState>();

        protected string NavMenuCSS { get; private set; }

        protected void ToggleNavMenu() => NavMenuCSS = NavMenuCSS == null ? "collapsed" : null;
        protected string ServerModeCSS() => State.BlazorMode == BlazorMode.ServerMode || State.BlazorMode == BlazorMode.Undefined ? "active" : "";
        protected string ClientModeCSS() => State.BlazorMode == BlazorMode.ClientMode ? "active" : "";

        protected async Task ActivateClientMode() {
            await RequestAsync(new SetBlazorModeRequest { BlazorMode = BlazorMode.ClientMode });

            await JSInteropHelpers.NavigateTo($"{UriHelper.GetBaseUri()}?mode=client");
        }

        protected async Task ActivateServerMode() {
            await RequestAsync(new SetBlazorModeRequest { BlazorMode = BlazorMode.ServerMode });

            await JSInteropHelpers.NavigateTo($"{UriHelper.GetBaseUri()}?mode=server");
        }

        /// <summary>
        ///     OnInit
        /// </summary>
        protected override async Task OnInitAsync() {
            var url = UriHelper.GetAbsoluteUri();

            if (IsServerMode(url))
                await RequestAsync(new SetBlazorModeRequest { BlazorMode = BlazorMode.ServerMode });

            else if (IsClientMode(url))
                await RequestAsync(new SetBlazorModeRequest { BlazorMode = BlazorMode.ClientMode });

            else
                await RequestAsync(new SetBlazorModeRequest { BlazorMode = BlazorMode.Undefined });
        }

        private static bool IsClientMode(string url) => Contains(url, "mode=client");

        private static bool IsServerMode(string url) => Contains(url, "mode=server");

        private static bool Contains(string url, string value) => CultureInfo.InvariantCulture.CompareInfo.IndexOf(url, value, CompareOptions.IgnoreCase) >= 0;
    }
}
