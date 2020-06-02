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
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class JSInteropHelpers
    {
        private const string BlazorDemo = "blazorDemo.";
        private static readonly ILogger Logger = ApplicationLogging.CreateLogger<JSInteropHelpers>();

        public static Action<Window>? OnWindowResized { get; set; }

        public static async Task<int> GetWindowWidthAsync(IJSRuntime jsRuntime) => await jsRuntime.InvokeAsync<int>($"{BlazorDemo}getWindowWidth");

        public static async Task InitializeWindowResizeEventAsync(IJSRuntime jsRuntime) =>
            await jsRuntime.InvokeAsync<object>($"{BlazorDemo}initializeWindowResizeEvent");

        public static async Task FocusAsync(IJSRuntime jsRuntime, ElementReference elementRef) =>
            await jsRuntime.InvokeAsync<bool>($"{BlazorDemo}focusElement", elementRef);

        public static async Task AlertAsync(IJSRuntime jsRuntime, string value) => await jsRuntime.InvokeAsync<bool>($"{BlazorDemo}showAlert", value);

        public static async Task NavigateToAsync(IJSRuntime jsRuntime, string url) => await jsRuntime.InvokeAsync<bool>($"{BlazorDemo}navigateTo", $"{url}");

        [JSInvokable]
        [UsedImplicitly]
        public static Task<string> FromJSWindowResizedAsync(Window window)
        {
            Guard.Against.Null(() => window);

            Logger.LogInformation("Window resized! Width: '{Width}', Height: '{Height}'!", window.Width, window.Height);

            Logger.LogInformation("SubscriberCount: {SubscriberCount}", OnWindowResized!.GetInvocationList().Length);

            OnWindowResized.Invoke(window);

            return Task.FromResult("Resize noticed!");
        }
    }
}