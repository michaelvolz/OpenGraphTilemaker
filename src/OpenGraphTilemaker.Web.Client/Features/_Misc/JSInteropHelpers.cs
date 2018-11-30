using System;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Common.Logging;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Blazor;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemaker.Web.Client.Features
{
    public static class ElementRefExtensions
    {
        public static async Task FocusAsync(this ElementRef elementRef) => await JSInteropHelpers.FocusAsync(elementRef);
    }

    public static class JSInteropHelpers
    {
        private const string BlazorDemo = "blazorDemo.";

        private static readonly ILogger Log = ApplicationLogging.CreateLogger(nameof(JSInteropHelpers));

        public static Action<Window> OnWindowResized { get; set; }

        public static bool IsRegistered(this Action<Window> handler, Action<Window> prospectiveHandler) =>
            handler != null && handler.GetInvocationList().Any(existingHandler => ReferenceEquals(existingHandler, prospectiveHandler));

        public static bool IsEventHandlerRegistered(Action<Window> prospectiveHandler) {
            if (OnWindowResized == null) return false;

            return OnWindowResized.GetInvocationList().Any(handler => handler.Equals(prospectiveHandler));
        }

        public static async Task<int> GetWindowWidthAsync() => await JSRuntime.Current.InvokeAsync<int>($"{BlazorDemo}getWindowWidth");

        public static async Task InitializeWindowResizeEvent() => await JSRuntime.Current.InvokeAsync<object>($"{BlazorDemo}initializeWindowResizeEvent");

        public static async Task FocusAsync(ElementRef elementRef) => await JSRuntime.Current.InvokeAsync<bool>($"{BlazorDemo}focusElement", elementRef);

        public static async Task AlertAsync(string value) => await JSRuntime.Current.InvokeAsync<bool>($"{BlazorDemo}showAlert", value);

        public static async Task NavigateToAsync(string url) => await JSRuntime.Current.InvokeAsync<bool>($"{BlazorDemo}navigateTo", $"{url}");

        [JSInvokable]
        [UsedImplicitly]
        public static Task<string> FromJSWindowResizedAsync([NotNull] Window window) {
            Guard.Against.Null(() => window);

            Log.LogInformation($"Window resized! Width: '{window.Width}', Height: '{window.Height}'!");

            Log.LogInformation($"SubscriberCount: {OnWindowResized.GetInvocationList().Length}");

            OnWindowResized?.Invoke(window);

            return Task.FromResult("Resize noticed!");
        }
    }
}
