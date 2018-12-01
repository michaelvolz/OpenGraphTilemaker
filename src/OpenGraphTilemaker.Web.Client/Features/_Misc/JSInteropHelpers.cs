using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Common.Logging;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Blazor;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using static Microsoft.JSInterop.JSRuntime;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemaker.Web.Client.Features
{
    public static class ElementRefExtensions
    {
        public static Task FocusAsync(this ElementRef elementRef) => JSInteropHelpers.FocusAsync(elementRef);
    }

    [UsedImplicitly]
    public class JSInteropHelpers
    {
        private const string BlazorDemo = "blazorDemo.";
        private static readonly ILogger Logger = ApplicationLogging.CreateLogger<JSInteropHelpers>();

        public static Action<Window> OnWindowResized { get; set; }

        public static Task<int> GetWindowWidthAsync() => Current.InvokeAsync<int>($"{BlazorDemo}getWindowWidth");
        public static Task InitializeWindowResizeEventAsync() => Current.InvokeAsync<object>($"{BlazorDemo}initializeWindowResizeEvent");
        public static Task FocusAsync(ElementRef elementRef) => Current.InvokeAsync<bool>($"{BlazorDemo}focusElement", elementRef);
        public static Task AlertAsync(string value) => Current.InvokeAsync<bool>($"{BlazorDemo}showAlert", value);
        public static Task NavigateToAsync(string url) => Current.InvokeAsync<bool>($"{BlazorDemo}navigateTo", $"{url}");

        [JSInvokable]
        [UsedImplicitly]
        public static Task<string> FromJSWindowResizedAsync([NotNull] Window window) {
            Guard.Against.Null(() => window);

            Logger.LogInformation($"Window resized! Width: '{window.Width}', Height: '{window.Height}'!");

            Logger.LogInformation($"SubscriberCount: {OnWindowResized.GetInvocationList().Length}");

            OnWindowResized?.Invoke(window);

            return Task.FromResult("Resize noticed!");
        }
    }
}
