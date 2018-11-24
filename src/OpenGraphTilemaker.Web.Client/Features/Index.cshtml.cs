using System.Threading.Tasks;

namespace OpenGraphTilemaker.Web.Client.Features
{
    public class IndexModel : BlazorComponentStateful
    {
        protected int WindowWidth { get; private set; }

        protected override async Task OnParametersSetAsync() {
            WindowWidth = await JSInteropHelpers.GetWindowWidthAsync();

            await JSInteropHelpers.OnParametersSet();
            JSInteropHelpers.OnWindowResized += WindowResized;

            await base.OnParametersSetAsync();
        }

        private void WindowResized(Window window) {
            WindowWidth = window.Width;
            StateHasChanged();
        }
    }
}
