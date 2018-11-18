using System;
using System.Linq;
using System.Threading.Tasks;
using BlazorState;
using MediatR;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.Extensions.Logging;

namespace OpenGraphTilemaker.Web.Client.Features
{
    public class BlazorComponentStateful : BlazorComponent, IBlazorStateComponent
    {
        private readonly Lazy<ILogger<BlazorComponentStateful>> _lazyLogger;

        protected BlazorComponentStateful() {
            _lazyLogger = new Lazy<ILogger<BlazorComponentStateful>>(() => LoggerFactory.CreateLogger<BlazorComponentStateful>());
        }

        public ILogger<BlazorComponentStateful> Log => _lazyLogger.Value;

        [Inject] public ILoggerFactory LoggerFactory { get; set; }
        [Inject] public IMediator Mediator { get; set; }
        [Inject] public IStore Store { get; set; }


        protected async Task RequestAsync<T>(IRequest<T> request) {
            await Mediator.Send(request);
        }
    }
}