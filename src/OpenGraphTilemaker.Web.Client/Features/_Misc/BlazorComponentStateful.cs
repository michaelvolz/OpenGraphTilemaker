using System;
using System.Threading.Tasks;
using BlazorState;
using Common;
using MediatR;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace OpenGraphTilemaker.Web.Client.Features
{
    public class BlazorComponentStateful : BlazorComponent
    {
        private readonly Lazy<ILogger<BlazorComponentStateful>> _lazyLogger;

        protected BlazorComponentStateful() {
            _lazyLogger = new Lazy<ILogger<BlazorComponentStateful>>(() => LoggerFactory.CreateLogger<BlazorComponentStateful>());
        }

        [Inject] public Time Time { get; set; }
        [Inject] public ILoggerFactory LoggerFactory { get; set; }

        public ILogger<BlazorComponentStateful> Log => _lazyLogger.Value;

        [Inject] public IMediator Mediator { get; set; }
        [Inject] public IStore Store { get; set; }
        [Inject] public IUriHelper UriHelper { get; set; }

        public int WindowWidth { get; set; }

        [JSInvokable]
        public Task<string> WindowResizedAsync(int windowWidth) {
            Console.WriteLine($"Window resized: new width: '{windowWidth}'!");
            WindowWidth = windowWidth;
            StateHasChanged();
            return Task.FromResult($"new WindowWidth: {windowWidth}");
        }

        protected async Task RequestAsync<T>(IRequest<T> request) {
            await Time.ThisAsync(async () => await Mediator.Send(request), request.GetType().Name);
        }

        protected override async Task OnParametersSetAsync() {
            await JSRuntime.Current.InvokeAsync<object>("blazorDemo.initialize", new DotNetObjectRef(this));
            await GetWindowWidth();
            base.OnParametersSet();
        }

        public async Task<int> GetWindowWidth() {
            WindowWidth = await JSRuntime.Current.InvokeAsync<int>("blazorDemo.getWindowsWidth");
            StateHasChanged();
            return WindowWidth;
        }
    }
}
