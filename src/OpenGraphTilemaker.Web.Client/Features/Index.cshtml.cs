using System;
using System.Threading.Tasks;

namespace OpenGraphTilemaker.Web.Client.Features
{
    public class IndexModel : BlazorComponentStateful, IDisposable
    {
        protected int WindowWidth { get; private set; }

        public void Dispose() {
            // ReSharper disable once DelegateSubtraction
            JSInteropHelpers.OnWindowResized -= WindowResized;
            Console.WriteLine("OnWindowResized event removed!");
        }

        protected override async Task OnParametersSetAsync() {
            WindowWidth = await JSInteropHelpers.GetWindowWidthAsync();

            await JSInteropHelpers.InitializeWindowResizeEvent();

            JSInteropHelpers.OnWindowResized += WindowResized;
            Console.WriteLine("OnWindowResized event added!");

            await base.OnParametersSetAsync();
        }

        private void WindowResized(Window window) {
            WindowWidth = window.Width;
            StateHasChanged();
        }
    }
}
