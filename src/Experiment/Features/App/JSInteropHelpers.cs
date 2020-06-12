using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Common.Logging;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Experiment.Features.App
{
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Utility class")]
    public sealed class JSInteropHelpers
    {
        private const string BlazorDemo = "blazorDemo.";
        private static readonly ILogger Logger = ApplicationLogging.CreateLogger<JSInteropHelpers>();

        public static Action<Window>? OnWindowResized { get; set; }

        public static async Task<int> GetWindowWidthAsync(IJSRuntime runtime) =>
            await runtime.InvokeAsync<int>($"{BlazorDemo}getWindowWidth").ConfigureAwait(false);

        public static async Task InitializeWindowResizeEventAsync(IJSRuntime runtime) =>
            await runtime.InvokeAsync<object>($"{BlazorDemo}initializeWindowResizeEvent").ConfigureAwait(false);

        public static async Task FocusAsync(IJSRuntime runtime, ElementReference elementRef) =>
            await runtime.InvokeAsync<bool>($"{BlazorDemo}focusElement", elementRef).ConfigureAwait(false);

        public static async Task AlertAsync(IJSRuntime runtime, string value) =>
            await runtime.InvokeAsync<bool>($"{BlazorDemo}showAlert", value).ConfigureAwait(false);

        [SuppressMessage("Design", "CA1054:Uri parameters should not be strings", Justification = "TODO")]
        public static async Task NavigateToAsync(IJSRuntime runtime, string url) =>
            await runtime.InvokeAsync<bool>($"{BlazorDemo}navigateTo", $"{url}").ConfigureAwait(false);

        [JSInvokable]
        [UsedImplicitly]
        public static Task<string> FromJSWindowResizedAsync(Window window)
        {
            Guard.Against.Null(window, nameof(window));

            Logger.LogInformation("Window resized! Width: '{Width}', Height: '{Height}'!", window.Width, window.Height);

            Logger.LogInformation("SubscriberCount: {SubscriberCount}", OnWindowResized!.GetInvocationList().Length);

            OnWindowResized.Invoke(window);

            return Task.FromResult("Resize noticed!");
        }
    }
}
