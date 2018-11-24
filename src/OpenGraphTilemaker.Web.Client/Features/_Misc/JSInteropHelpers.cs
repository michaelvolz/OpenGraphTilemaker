using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

// ReSharper disable MemberCanBePrivate.Global

namespace OpenGraphTilemaker.Web.Client.Features
{
    public static class JSInteropHelpers
    {
        public static async Task<int> GetWindowWidthAsync() {
            return await JSRuntime.Current.InvokeAsync<int>("blazorDemo.getWindowWidth");
        }

        public static async Task<JSState> OnParametersSet() {
            var jsState = new JSState();
            await JSRuntime.Current.InvokeAsync<object>("blazorDemo.onParametersSet", new DotNetObjectRef(jsState));
            jsState.WindowWidth = await GetWindowWidthAsync();

            return jsState;
        }

        public class JSState
        {
            public Action OnWindowResized;

            public int WindowWidth { get; set; }

            [JSInvokable]
            public Task<string> FromJSWindowResizedAsync(int windowWidth) {
                Console.WriteLine($"Window resized: new width: '{windowWidth}'!");
                WindowWidth = windowWidth;

                OnWindowResized();

                return Task.FromResult($"new WindowWidth: {windowWidth}");
            }
        }
    }
}
