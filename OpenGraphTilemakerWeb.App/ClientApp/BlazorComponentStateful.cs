using System;
using BlazorState;
using MediatR;
using Microsoft.AspNetCore.Blazor.Components;

namespace OpenGraphTilemakerWeb.App.ClientApp
{
    public class BlazorComponentStateful : BlazorComponent, IBlazorStateComponent
    {
        [Inject] public IMediator Mediator { get; set; }
        [Inject] public IStore Store { get; set; }

        protected void Request<T>(IRequest<T> request)
        {
            var send = Mediator.Send(request);

            if (send.IsFaulted)
                throw new InvalidOperationException(send.Exception?.Message);
        }
    }
}