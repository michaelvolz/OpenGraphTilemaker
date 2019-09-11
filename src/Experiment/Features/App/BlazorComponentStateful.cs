using System;
using System.Threading.Tasks;
using BlazorState;
using Common;
using Common.Logging;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable UnusedMember.Global

namespace Experiment.Features.App
{
    public class BlazorComponentStateful<TComponent> : ComponentBase
    {
#nullable disable
        [Inject] private IMediator Mediator { get; set; }

        [Inject] protected Time Time { get; set; }
        [Inject] protected IStore Store { get; set; }
        [Inject] protected NavigationManager UriHelper { get; set; }

        [Inject] protected IJSRuntime JSRuntime { get; set; }
        // [Inject] protected IComponentContext ComponentContext { get; set; }
#nullable enable

        protected ILogger<TComponent> Logger { get; } = ApplicationLogging.CreateLogger<TComponent>();

        protected bool IsLoading { get; set; } = true;

        protected string? HideIf(Func<bool> predicate) => predicate() ? "collapsed" : null;
        protected string? ShowIf(Func<bool> predicate) => predicate() ? null : "collapsed";

        protected async Task<TRequest> RequestAsync<TRequest>(IRequest<TRequest> request) =>
            await Mediator.Send(request);
    }
}