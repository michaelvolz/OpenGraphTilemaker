using System;
using System.Linq;
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


        protected void Request<T>(IRequest<T> request) {
            var send = Mediator.Send(request);

            if (send.IsFaulted) {
                var exception = send.Exception;
                Log.LogError(exception, $"### Invalid MediatR request of '{request.GetType().Name}'!{Environment.NewLine}");
            }
        }
    }
}