using System;
using System.Threading.Tasks;
using BlazorState;
using Common;
using MediatR;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.Extensions.Logging;

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

        protected async Task RequestAsync<T>(IRequest<T> request) {
            await Time.ThisAsync(async () => await Mediator.Send(request), request.GetType().Name);
        }
    }
}
