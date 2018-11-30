using System;
using System.Threading.Tasks;
using Common.Exceptions;
using Microsoft.Extensions.Logging;

#pragma warning disable CA1063 // Modify IndexModel.Finalize so that it calls Dispose(false) and then returns.
#pragma warning disable CA1821 // Remove empty Finalizers

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
            Log.LogInformation("OnWindowResized event added!");

            NestedExceptionLoggingTest();

            await base.OnParametersSetAsync();
        }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                // ReSharper disable once DelegateSubtraction
                JSInteropHelpers.OnWindowResized -= WindowResized;
                Log.LogInformation("OnWindowResized event removed!");
            }
        }

        private void NestedExceptionLoggingTest() {
            try {
                try {
                    try {
                        throw new InvalidOperationException("Inner Test Exception");
                    }
                    catch (Exception e) when (!(e is LoggedException)) {
                        var errorMsg = "An INNER TEST ERROR occurred ---some details---";
                        throw new LoggedException(typeof(IndexModel), e, errorMsg);
                    }
                }
                catch (Exception e) when (!(e is LoggedException)) {
                    var errorMsg = "THIS SHOULD NEVER BE LOGGED! An OUTER error occurred ---some details---";
                    throw new LoggedException(typeof(IndexModel), e, errorMsg);
                }
            }
            catch (Exception e) {
                Log.LogWarning(e, "Oops!");
            }
        }

        private void WindowResized(Window window) {
            WindowWidth = window.Width;
            StateHasChanged();
        }

        ~IndexModel() => Dispose(false);
    }
}
