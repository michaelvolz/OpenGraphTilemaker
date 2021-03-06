﻿using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using BlazorState;
using Common;
using Common.Logging;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Experiment.Features.AppCode
{
    public class BlazorComponentStateful<TComponent> : ComponentBase
    {
        protected ILogger<TComponent> Logger { get; } = ApplicationLogging.CreateLogger<TComponent>();
        [Inject] protected Time Time { get; [UsedImplicitly] set; } = null!;
        [Inject] protected IStore Store { get; [UsedImplicitly] set; } = null!;
        [Inject] protected NavigationManager UriHelper { get; [UsedImplicitly] set; } = null!;
        [Inject] protected IJSRuntime JSRuntime { get; [UsedImplicitly] set; } = null!;

        protected bool IsLoading { get; set; } = true;
        [Inject] private IMediator Mediator { get; [UsedImplicitly] set; } = null!;

        protected string? HideIf(Func<bool> predicate) => Guard.Against.Null(predicate, nameof(predicate))() ? "collapsed" : null;
        protected string? ShowIf(Func<bool> predicate) => Guard.Against.Null(predicate, nameof(predicate))() ? null : "collapsed";

        protected async Task<TRequest> RequestAsync<TRequest>(IRequest<TRequest> request) =>
            await Mediator.Send(request);
    }
}
