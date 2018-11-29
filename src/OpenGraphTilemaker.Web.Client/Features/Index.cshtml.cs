using System;
using System.Threading.Tasks;

namespace OpenGraphTilemaker.Web.Client.Features
{
    public class IndexModel : BlazorComponentStateful, IDisposable
    {
        protected int WindowWidth { get; private set; }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override async Task OnParametersSetAsync() {
            WindowWidth = await JSInteropHelpers.GetWindowWidthAsync();

            await JSInteropHelpers.InitializeWindowResizeEvent();

            JSInteropHelpers.OnWindowResized += WindowResized;
            Console.WriteLine("OnWindowResized event added!");

            await base.OnParametersSetAsync();
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                // ReSharper disable once DelegateSubtraction
                JSInteropHelpers.OnWindowResized -= WindowResized;
                Console.WriteLine("OnWindowResized event removed!");
            }
        }

        private void WindowResized(Window window) {
            WindowWidth = window.Width;
            StateHasChanged();
        }
#pragma warning disable CA1063 // Modify IndexModel.Finalize so that it calls Dispose(false) and then returns.
#pragma warning disable CA1821 // Remove empty Finalizers
        ~IndexModel() => Dispose(false);
    }
}
