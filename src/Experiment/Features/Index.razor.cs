using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Exceptions;
using Experiment.Features.App;
using Experiment.Features.App.Globals;
using Microsoft.Extensions.Logging;
using GlobalState = Experiment.Features.App.Globals.GlobalState;

// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CA1063 // Modify TilesPageModel.Finalize so that it calls Dispose(false) and then returns.
#pragma warning disable CA1821 // Remove empty Finalizers
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize

namespace Experiment.Features
{
    public class IndexModel : BlazorComponentStateful<IndexModel>, IDisposable
    {
        private const string ThisShouldNeverBeLogged = "THIS SHOULD NEVER BE LOGGED!";
        private bool _initialized;

        protected GlobalState GlobalState => Store.GetState<GlobalState>();

        protected int WindowWidth { get; private set; } = -1;

        public void Dispose()
        {
            // ReSharper disable once DelegateSubtraction
            JSInteropHelpers.OnWindowResized -= WindowResized;
            Logger.LogInformation("OnWindowResized event removed!");
        }

        protected async Task ChangeThemeColors()
        {
            var request = new ChangeThemeColorsRequest
            {
                ThemeColor1 = GlobalState.ThemeColor2,
                ThemeColor2 = GlobalState.ThemeColor3,
                ThemeColor3 = GlobalState.ThemeColor1
            };

            await RequestAsync(request);
        }

        protected override async Task OnAfterRenderAsync()
        {
            await base.OnAfterRenderAsync();

            await InitializeJavaScriptEvents();
        }

        private async Task InitializeJavaScriptEvents()
        {
            if (!_initialized)
            {
                await JSInteropHelpers.InitializeWindowResizeEventAsync(ComponentContext, JSRuntime);

                JSInteropHelpers.OnWindowResized += WindowResized;
                Logger.LogInformation("OnWindowResized event added!");

                WindowWidth = await JSInteropHelpers.GetWindowWidthAsync(ComponentContext, JSRuntime);

                _initialized = true;

                StateHasChanged();
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            NestedExceptionLoggingTest();

            await base.OnParametersSetAsync();
        }

        private void NestedExceptionLoggingTest()
        {
            try
            {
                try
                {
                    try
                    {
                        using (Logger.BeginScope(new Dictionary<string, object>
                        { ["CustomerId"] = 12345, ["OrderId"] = 54 }))
                        {
                            Logger.LogInformation("Processing credit card payment...");

                            throw new InvalidOperationException("Inner Test Exception!");
                        }
                    }
                    catch (Exception e) when (e.LogException<IndexModel>())
                    {
                        throw new LoggedException(e);
                    }
                }
                catch (Exception e) when (e.LogException<IndexModel>())
                {
                    throw new LoggedException(ThisShouldNeverBeLogged + " An OUTER error occurred!", e);
                }
            }
            catch (Exception e)
            {
                if (e.ToString().Contains(ThisShouldNeverBeLogged, StringComparison.InvariantCulture)) Logger.LogWarning(e, "Oops!");
            }
        }

        private void WindowResized(Window window)
        {
            WindowWidth = window.Width;
            StateHasChanged();
        }
    }
}