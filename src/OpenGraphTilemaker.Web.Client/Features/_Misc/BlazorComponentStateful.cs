using System;
using System.Threading.Tasks;
using BlazorState;
using Common;
using MediatR;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using Microsoft.Extensions.Logging;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable UnusedMember.Global

namespace OpenGraphTilemaker.Web.Client.Features
{
    public class BlazorComponentStateful : BlazorComponent
    {
        private readonly Lazy<ILogger<BlazorComponentStateful>> _lazyLogger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BlazorComponentStateful" /> class.
        /// </summary>
        protected BlazorComponentStateful() =>
            _lazyLogger = new Lazy<ILogger<BlazorComponentStateful>>(() => LoggerFactory.CreateLogger<BlazorComponentStateful>());

        [Inject] private ILoggerFactory LoggerFactory { get; set; }
        [Inject] private IMediator Mediator { get; set; }

        [Inject] protected Time Time { get; set; }
        [Inject] protected IStore Store { get; set; }
        [Inject] protected IUriHelper UriHelper { get; set; }

        protected ILogger<BlazorComponentStateful> Log => _lazyLogger.Value;

        protected bool IsLoading { get; set; } = true;

        protected string HideIf(Func<bool> predicate) => predicate() ? "collapsed" : null;
        protected string ShowIf(Func<bool> predicate) => predicate() ? null : "collapsed";

        protected async Task RequestAsync<T>(IRequest<T> request) => await Mediator.Send(request);
    }
}
