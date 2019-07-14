using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Common;
using Common.Logging;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

// ReSharper disable MemberCanBePrivate.Global

namespace Experiment.Features
{
    [UsedImplicitly]
    public class JSInteropHelpers
    {
        private const string BlazorDemo = "blazorDemo.";
        private static readonly ILogger Logger = ApplicationLogging.CreateLogger<JSInteropHelpers>();

        public static Action<Window> OnWindowResized { get; set; }

        public static async Task<int> GetWindowWidthAsync(IComponentContext componentContext, IJSRuntime jsRuntime)
        {
            if (!componentContext.IsConnected) throw new InvalidOperationException("!componentContext.IsConnected");
            
            return await jsRuntime.InvokeAsync<int>($"{BlazorDemo}getWindowWidth");
        }

        public static async Task InitializeWindowResizeEventAsync(IComponentContext componentContext, IJSRuntime jsRuntime)
        {
            if (!componentContext.IsConnected) throw new InvalidOperationException("!componentContext.IsConnected");

            await jsRuntime.InvokeAsync<object>($"{BlazorDemo}initializeWindowResizeEvent");
        }

        public static async Task FocusAsync(IComponentContext componentContext, IJSRuntime jsRuntime, ElementRef elementRef)
        {
            if (!componentContext.IsConnected) throw new InvalidOperationException("!componentContext.IsConnected");
            
            await jsRuntime.InvokeAsync<bool>($"{BlazorDemo}focusElement", elementRef);
        }

        public static async Task AlertAsync(IComponentContext componentContext, IJSRuntime jsRuntime, string value)
        {
            if (!componentContext.IsConnected) throw new InvalidOperationException("!componentContext.IsConnected");
            
            await jsRuntime.InvokeAsync<bool>($"{BlazorDemo}showAlert", value);
        }

        public static async Task NavigateToAsync(IComponentContext componentContext, IJSRuntime jsRuntime, string url)
        {
            if (!componentContext.IsConnected) throw new InvalidOperationException("!componentContext.IsConnected");

            await jsRuntime.InvokeAsync<bool>($"{BlazorDemo}navigateTo", $"{url}");
        }

        [JSInvokable]
        [UsedImplicitly]
        public static Task<string> FromJSWindowResizedAsync([NotNull] Window window)
        {
            Guard.Against.Null(() => window);

            Logger.LogInformation($"Window resized! Width: '{window.Width}', Height: '{window.Height}'!");

            Logger.LogInformation($"SubscriberCount: {OnWindowResized.GetInvocationList().Length}");

            OnWindowResized?.Invoke(window);

            return Task.FromResult("Resize noticed!");
        }
    }
}