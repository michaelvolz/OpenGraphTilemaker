﻿using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Common.Logging;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using static Microsoft.JSInterop.JSRuntime;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemaker.Web.Client.Features
{
    [UsedImplicitly]
    public class JSInteropHelpers
    {
        private const string BlazorDemo = "blazorDemo.";
        private static readonly ILogger Logger = ApplicationLogging.CreateLogger<JSInteropHelpers>();

        public static Action<Window> OnWindowResized { get; set; }

        public static async Task<int> GetWindowWidthAsync() => await Current.InvokeAsync<int>($"{BlazorDemo}getWindowWidth");
        public static async Task InitializeWindowResizeEventAsync() => await Current.InvokeAsync<object>($"{BlazorDemo}initializeWindowResizeEvent");
        public static async Task FocusAsync(ElementRef elementRef) => await Current.InvokeAsync<bool>($"{BlazorDemo}focusElement", elementRef);
        public static async Task AlertAsync(string value) => await Current.InvokeAsync<bool>($"{BlazorDemo}showAlert", value);
        public static async Task NavigateToAsync(string url) => await Current.InvokeAsync<bool>($"{BlazorDemo}navigateTo", $"{url}");

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
