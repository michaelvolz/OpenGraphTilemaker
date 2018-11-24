using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using JetBrains.Annotations;
using Microsoft.JSInterop;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemaker.Web.Client.Features
{
    public static class JSInteropHelpers
    {
        [UsedImplicitly] public static Action<Window> OnWindowResized;

        public static async Task<int> GetWindowWidthAsync() {
            return await JSRuntime.Current.InvokeAsync<int>("blazorDemo.getWindowWidth");
        }

        public static async Task OnParametersSet() {
            await JSRuntime.Current.InvokeAsync<object>("blazorDemo.onParametersSet");
        }

        [JSInvokable]
        [UsedImplicitly]
        public static Task<string> FromJSWindowResizedAsync([NotNull] Window window) {
            Guard.Against.Null(() => window);

            Console.WriteLine($"Window resized: new width: '{window.Width}'!");

            OnWindowResized(window);

            return Task.FromResult($"new WindowWidth: {window.Width}");
        }
    }

    public class Window
    {
        public int Height;
        public int Width;
    }
}
