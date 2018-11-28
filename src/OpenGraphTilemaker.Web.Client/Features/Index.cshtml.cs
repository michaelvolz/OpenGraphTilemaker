using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace OpenGraphTilemaker.Web.Client.Features
{
    public class IndexModel : BlazorComponentStateful, IDisposable
    {
        protected int WindowWidth { get; private set; }

        [SuppressMessage("ReSharper", "DelegateSubtraction")]
        public void Dispose() {
            JSInteropHelpers.OnWindowResized -= WindowResized;
            Console.WriteLine("OnWindowResized event removed!");
        }

        protected override async Task OnParametersSetAsync() {
            WindowWidth = await JSInteropHelpers.GetWindowWidthAsync();

            await JSInteropHelpers.InitializeWindowResizeEvent();

            if (JSInteropHelpers.IsEventHandlerRegistered(WindowResized)) {
                Console.WriteLine("!!! Already registered!");
            }
            
            if (JSInteropHelpers.OnWindowResized.IsRegistered(WindowResized)) {
                Console.WriteLine("!!! Already registered!");
            }

            // ReSharper disable once DelegateSubtraction
            JSInteropHelpers.OnWindowResized -= WindowResized;
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
