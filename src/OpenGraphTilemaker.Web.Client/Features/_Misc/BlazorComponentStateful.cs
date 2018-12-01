using System;
using System.Threading.Tasks;
using BlazorState;
using Common;
using Common.Logging;
using MediatR;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using Microsoft.Extensions.Logging;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable UnusedMember.Global

namespace OpenGraphTilemaker.Web.Client.Features
{
    public class BlazorComponentStateful<TComponent> : BlazorComponent
    {
        [Inject] private IMediator Mediator { get; set; }

        [Inject] protected Time Time { get; set; }
        [Inject] protected IStore Store { get; set; }
        [Inject] protected IUriHelper UriHelper { get; set; }

        protected ILogger<TComponent> Logger { get; set; } = ApplicationLogging.CreateLogger<TComponent>();

        protected bool IsLoading { get; set; } = true;

        protected string HideIf(Func<bool> predicate) => predicate() ? "collapsed" : null;
        protected string ShowIf(Func<bool> predicate) => predicate() ? null : "collapsed";

        protected async Task<TRequest> RequestAsync<TRequest>(IRequest<TRequest> request) => await Mediator.Send(request);
    }
}
