using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Exceptions;
using Microsoft.Extensions.Logging;
using OpenGraphTilemaker.Web.Client.Features.Globals;

// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CA1063 // Modify TilesPageModel.Finalize so that it calls Dispose(false) and then returns.
#pragma warning disable CA1821 // Remove empty Finalizers
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize

namespace OpenGraphTilemaker.Web.Client.Features
{
    public class IndexModel : BlazorComponentStateful<IndexModel>, IDisposable
    {
        private const string ThisShouldNeverBeLogged = "THIS SHOULD NEVER BE LOGGED!";

        protected Globals.Globals MyGlobals { get; set; }

        protected GlobalState GlobalState => Store.GetState<GlobalState>();

        protected int WindowWidth { get; private set; }

        public void Dispose() {
            // ReSharper disable once DelegateSubtraction
            JSInteropHelpers.OnWindowResized -= WindowResized;
            Logger.LogInformation("OnWindowResized event removed!");
        }

        protected async Task ChangeThemeColors() {
            var request = new ChangeThemeColorsRequest {
                ThemeColor1 = GlobalState.ThemeColor2,
                ThemeColor2 = GlobalState.ThemeColor3,
                ThemeColor3 = GlobalState.ThemeColor1
            };

            await RequestAsync(request);
        }

        protected override async Task OnInitAsync() {
            WindowWidth = await JSInteropHelpers.GetWindowWidthAsync();

            await JSInteropHelpers.InitializeWindowResizeEventAsync();

            JSInteropHelpers.OnWindowResized += WindowResized;
            Logger.LogInformation("OnWindowResized event added!");

            await base.OnInitAsync();
        }

        protected override async Task OnParametersSetAsync() {
            await base.OnParametersSetAsync();

            NestedExceptionLoggingTest();
        }

        private void NestedExceptionLoggingTest() {
            try {
                try {
                    try {
                        using (Logger.BeginScope(new Dictionary<string, object> { ["CustomerId"] = 12345, ["OrderId"] = 54 })) {
                            Logger.LogInformation("Processing credit card payment...");

                            throw new InvalidOperationException("Inner Test Exception!");
                        }
                    }
                    catch (Exception e) when (e.LogException<IndexModel>()) {
                        throw new LoggedException(e);
                    }
                }
                catch (Exception e) when (e.LogException<IndexModel>()) {
                    throw new LoggedException(ThisShouldNeverBeLogged + " An OUTER error occurred!", e);
                }
            }
            catch (Exception e) {
                if (e.ToString().Contains(ThisShouldNeverBeLogged)) Logger.LogWarning(e, "Oops!");
            }
        }

        private void WindowResized(Window window) {
            WindowWidth = window.Width;
            StateHasChanged();
        }
    }
}
