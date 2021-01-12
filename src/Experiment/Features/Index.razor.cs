using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Common.Exceptions;
using Experiment.Features.AppCode;
using Experiment.Features.AppCode.GlobalsControl;
using Microsoft.Extensions.Logging;

namespace Experiment.Features
{
    public sealed partial class Index : IDisposable
    {
        private const string ThisShouldNeverBeLogged = "THIS SHOULD NEVER BE LOGGED!";
        private bool _initialized;

        private GlobalState GlobalState => Store.GetState<GlobalState>();

        private int WindowWidth { get; set; } = -1;

        public void Dispose()
        {
            if (JSInteropHelpers.OnWindowResized != null) JSInteropHelpers.OnWindowResized -= WindowResized;
            Logger.LogInformation("OnWindowResized event removed!");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            await InitializeJavaScriptEvents();
        }

        protected override async Task OnParametersSetAsync()
        {
            NestedExceptionLoggingTest();

            await base.OnParametersSetAsync();
        }

        private async Task ChangeThemeColors()
        {
            var request = new GlobalState.ChangeThemeColorsRequest
            {
                ThemeColor1 = GlobalState.ThemeColor2,
                ThemeColor2 = GlobalState.ThemeColor3,
                ThemeColor3 = GlobalState.ThemeColor1
            };

            await RequestAsync(request);
        }

        private async Task InitializeJavaScriptEvents()
        {
            if (!_initialized)
            {
                await JSInteropHelpers.InitializeWindowResizeEventAsync(JSRuntime);

                JSInteropHelpers.OnWindowResized += WindowResized;
                Logger.LogInformation("OnWindowResized event added!");

                WindowWidth = await JSInteropHelpers.GetWindowWidthAsync(JSRuntime);

                _initialized = true;

                StateHasChanged();
            }
        }

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Test")]
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
                    catch (Exception e) when (e.LogException<Index>())
                    {
                        throw new LoggedException(e);
                    }
                }
                catch (Exception e) when (e.LogException<Index>())
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
